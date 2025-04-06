from bs4 import BeautifulSoup
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import WebDriverWait
import time
from bs4 import SoupStrainer
import json
import os

def _processar_celula(texto):
    texto = texto.split('(')[0].strip()
    return ' '.join(texto.split())


class CaptureEvents:
    def __init__(self, driver, processos):
        self.driver = driver
        self.processos = processos
        self.max_retries = 1
        self.base_url = self.driver.current_url

    @staticmethod
    def _criar_pasta_asserts():
        # Cria a pasta asserts se não existir
        os.makedirs('asserts', exist_ok=True)

    @staticmethod
    def _salvar_json(resultado):
        # Gera nome do arquivo válido
        nome_arquivo = resultado['numero_processo'].replace('/', '_').replace('.', '_') + '.json'
        caminho = os.path.join('asserts', nome_arquivo)

        # Salva o arquivo com formatação
        with open(caminho, 'w', encoding='utf-8') as f:
            json.dump(resultado, f, ensure_ascii=False, indent=4)

    def executar(self):
        resultados = []
        for processo in self.processos:
            resultado = self._processar_processo(processo)
            resultados.append(resultado)
            
        return resultados

    def _navegar_para_detalhes_processo(self, processo):
        try:
            # Salva a janela original
            original_window = self.driver.current_window_handle

            # Localiza e clica no link do processo
            link = WebDriverWait(self.driver, 15).until(
                EC.element_to_be_clickable((By.LINK_TEXT, processo.numero_processo.strip()))
            )
            self.driver.execute_script("arguments[0].scrollIntoView({behavior: 'instant', block: 'center'});", link)
            link.click()

            # Aguarda a abertura de nova aba e muda o foco
            WebDriverWait(self.driver, 15).until(EC.number_of_windows_to_be(2))
            new_window = [window for window in self.driver.window_handles if window != original_window][0]
            self.driver.switch_to.window(new_window)

            # Aguarda a tabela de eventos carregar
            WebDriverWait(self.driver, 15).until(
                EC.presence_of_element_located((By.ID, 'tblEventos'))
            )
            return True

        except Exception as e:
            print(f"Erro na navegação: {str(e)}")
            return False

    def _extrair_eventos_pagina(self):
        try:
            # Garante que a tabela está presente
            WebDriverWait(self.driver, 5).until(
                EC.presence_of_element_located((By.ID, 'tblEventos'))
            )

            soup_filter = SoupStrainer('table', {'id': 'tblEventos'})
            soup = BeautifulSoup(self.driver.page_source, 'lxml', parse_only=soup_filter)
            tabela = soup.find('table', {'id': 'tblEventos'})

            if not tabela:
                print("Tabela 'Eventos' não encontrada.")
                return []

            # Extrai os eventos
            eventos = [
                [cell.get_text(separator='\n', strip=True) for cell in row.find_all('td')]                 
                for row in tabela.find_all('tr')
            ]

            return eventos

        except Exception as e:
            print(f"Erro na extração: {str(e)}")
            return []

    def _processar_processo(self, processo):
        original_window = self.driver.current_window_handle
        for tentativa in range(self.max_retries):
            try:
                if not self._navegar_para_detalhes_processo(processo):
                    continue
    
                eventos = self._extrair_eventos_pagina()
    
                # Fecha a nova aba e retorna para a original
                self.driver.close()
                self.driver.switch_to.window(original_window)
    
                resultado = {
                    "numero_processo": processo.numero_processo,
                    "status": "SUCESSO",
                    "tentativas": tentativa + 1,
                    "eventos": eventos  # Agora como último campo
                }
    
                # Salva o JSON na pasta asserts
                self._salvar_json(resultado)
                return resultado
    
            except Exception as e:
                print(f"Tentativa {tentativa+1} falhou: {str(e)}")
                if len(self.driver.window_handles) > 1:
                    self.driver.close()
                    self.driver.switch_to.window(original_window)
                else:
                    self.driver.get(self.base_url)
    
        resultado = {
            "numero_processo": processo.numero_processo,
            "status": "FALHA",
            "tentativas": self.max_retries,
            "eventos": []  # Último campo mesmo quando vazio
        }
        self._salvar_json(resultado)
        return resultado
        