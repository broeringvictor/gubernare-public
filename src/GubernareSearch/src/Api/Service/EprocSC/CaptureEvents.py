import time
from selenium.common import TimeoutException, StaleElementReferenceException
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import WebDriverWait
from src.Domain.Entities.EventoEntity import EventoEntity

class CaptureEvents:
    def __init__(self, driver, processos):
        self.driver = driver
        self.processos = processos
        self.eventos_coletados = []
        self.max_retries = 3
        self.base_url = "URL_DA_PAGINA_DE_CONSULTA"  # Atualize com a URL correta

    def _navegar_para_detalhes_processo(self, processo):
        """
        Navega até a página de detalhes do processo clicando no link que
        possui o texto visível igual a processo.numero_processo.
        Retorna True se a navegação for bem-sucedida, ou False em caso de falha.
        """
        for attempt in range(self.max_retries):
            try:
                # Localiza o link pelo texto visível
                link = WebDriverWait(self.driver, 15).until(
                    EC.element_to_be_clickable((By.LINK_TEXT, processo.numero_processo.strip()))
                )

                # Rolagem e clique no link
                self.driver.execute_script(
                    "arguments[0].scrollIntoView({behavior: 'instant', block: 'center'});", link
                )
                self.driver.execute_script("arguments[0].click();", link)

                # Se o link abre em nova aba, muda o foco para ela
                if len(self.driver.window_handles) > 1:
                    self.driver.switch_to.window(self.driver.window_handles[-1])

                # Aguarda que a tabela de eventos esteja presente
                WebDriverWait(self.driver, 20).until(
                    lambda d: d.find_element(By.CSS_SELECTOR, "#tblEventos")
                )
                return True

            except Exception as e:
                print(f"Erro de navegação ({attempt+1}/{self.max_retries}): {str(e)}")
                if attempt == self.max_retries - 1:
                    return False
                self.driver.refresh()
                time.sleep(2)

    def _extrair_eventos_pagina(self):
        """Extrai todas as linhas da tabela de eventos sem distinção."""
        try:
            time.sleep(2)  # Pausa para garantir o carregamento da página
            print("Iniciando a extração dos dados de eventos...")
            WebDriverWait(self.driver, 25).until(
                EC.visibility_of_element_located((By.CSS_SELECTOR, "#tblEventos > tbody"))
            )
            rows = self.driver.find_elements(By.CSS_SELECTOR, "#tblEventos > tbody > tr")
            eventos = [row.get_attribute("outerHTML") for row in rows if row.is_displayed()]
            return eventos

        except TimeoutException:
            print("Nenhum evento encontrado na página")
            return []
        except Exception as e:
            print(f"Erro crítico na extração: {str(e)}")
            return []

    def _processar_processo(self, processo):
        """Fluxo principal com tratamento de erro reforçado"""
        for attempt in range(self.max_retries):
            try:
                if not self._navegar_para_detalhes_processo(processo):
                    continue

                eventos = self._extrair_eventos_pagina()

                # Volta para a página de listagem de processos
                self.driver.back()
                WebDriverWait(self.driver, 20).until(
                    EC.presence_of_element_located((By.CSS_SELECTOR, "table.lista-processos"))
                )

                return {
                    "numero_processo": processo.numero_processo,
                    "eventos": eventos,
                    "status": "SUCESSO",
                    "tentativas": attempt + 1
                }

            except Exception as e:
                print(f"Tentativa {attempt+1} falhou: {str(e)}")
                if attempt == self.max_retries - 1:
                    return {
                        "numero_processo": processo.numero_processo,
                        "eventos": [],
                        "status": "FALHA",
                        "tentativas": attempt + 1
                    }
                self.driver.get(self.base_url)
                time.sleep(2)

        return {
            "numero_processo": processo.numero_processo,
            "eventos": [],
            "status": "FALHA",
            "tentativas": self.max_retries
        }

    def execute(self):
        """Execução principal com tratamento de erro no relatório"""
        if not self.processos:
            print("Nenhum processo disponível para captura.")
            return []

        print(f"\n🏁 Iniciando captura para {len(self.processos)} processos")
        resultados = []
        for idx, processo in enumerate(self.processos, 1):
            print(f"\n📑 Processando ({idx}/{len(self.processos)}) {processo.numero_processo}")
            start_time = time.time()
            resultado = self._processar_processo(processo)
            elapsed = time.time() - start_time

            resultado["tempo_processamento"] = f"{elapsed:.2f}s"
            resultados.append(resultado)
            print(f"{'🟢' if resultado['status']=='SUCESSO' else '🔴'} {resultado['status']} | Tentativas: {resultado['tentativas']} | Eventos: {len(resultado['eventos'])} | Tempo: {elapsed:.2f}s")

        print("\n📊 Resumo Final:")
        for res in resultados:
            print(f"• {res['numero_processo']}: {res['status']} ({res['tempo_processamento']}) - {len(res['eventos'])} eventos")
        return resultados
