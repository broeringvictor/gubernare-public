import time
from bs4 import BeautifulSoup
from selenium.common import TimeoutException, StaleElementReferenceException, WebDriverException
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import WebDriverWait

class CaptureEvents:
    def __init__(self, driver, processos):
        self.driver = driver
        self.processos = processos
        self.max_retries = 1
        self.base_url = self.driver.current_url

    def _navegar_para_detalhes_processo(self, processo):
        """
        Navega até a página de detalhes do processo clicando no link que
        possui o texto visível igual a processo.numero_processo.
        Retorna True se a navegação for bem-sucedida, ou False em caso de falha.
        """
        for attempt in range(self.max_retries):
            try:
                link = WebDriverWait(self.driver, 15).until(
                    EC.element_to_be_clickable((By.LINK_TEXT, processo.numero_processo.strip()))
                )
                self.driver.execute_script(
                    "arguments[0].scrollIntoView({behavior: 'instant', block: 'center'});", link
                )
                self.driver.execute_script("arguments[0].click();", link)


                return True

            except Exception as e:
                print(f"Erro de navegação ({attempt+1}/{self.max_retries}): {str(e)}")
                if attempt == self.max_retries - 1:
                    return False
                # Tenta recarregar a página de listagem de processos usando a URL atual
                try:
                    self.driver.get(self.base_url)
                except WebDriverException as get_e:
                    print(f"Erro ao recarregar a página: {str(get_e)}")
                time.sleep(2)
    def _extrair_eventos_pagina(self):
        """Extrai todas as linhas da tabela de eventos."""
        try:
            from bs4 import BeautifulSoup
            import time

            # Aguarda carregamento da tabela
            time.sleep(2)
            html = self.driver.page_source
            soup = BeautifulSoup(html, 'html.parser')

            # Localiza a tabela de eventos pelo ID 'tblEventos'
            tabela = soup.find('table', summary="Eventos")
            if tabela:

                rows = tabela.find_all('tr')

                # Inicializa uma lista para armazenar os dados
                data = []

                # Itera sobre as linhas, começando da segunda (índice 1) para ignorar o cabeçalho
                for row in rows[1:]:
                    # Encontra todas as células da linha
                    cells = row.find_all('td')

                    # Extrai o texto de cada célula e adiciona à lista de dados
                    data.append([cell.get_text(strip=True) for cell in cells])

                return data

        except Exception as e:
                print(f"Erro ao extrair eventos: {str(e)}")
                return []


    def _processar_processo(self, processo):
        """Fluxo principal com tratamento de erro reforçado para um processo."""
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
                try:
                    self.driver.get(self.base_url)
                except WebDriverException as get_e:
                    print(f"Erro ao recarregar a página: {str(get_e)}")
                time.sleep(2)

        return {
            "numero_processo": processo.numero_processo,
            "eventos": [],
            "status": "FALHA",
            "tentativas": self.max_retries
        }

    def execute(self):
        """Execução principal com tratamento de erro no relatório."""
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
