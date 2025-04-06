import re

from dotenv import load_dotenv
from selenium.common.exceptions import TimeoutException, NoSuchElementException, WebDriverException
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import WebDriverWait
from bs4 import BeautifulSoup, SoupStrainer
from src.Domain.Entities.EprocOverviewEntity import EprocOverviewEntity

load_dotenv()

class LegalCasesOverviewActivity:
    def __init__(self, driver):
        self.driver = driver
        self.lista_processos = []

    def _handle_captcha(self):
        """Verifica a presença de CAPTCHA na página atual"""
        try:
            # Modifique este seletor conforme o CAPTCHA do seu sistema
            captcha_frame = WebDriverWait(self.driver, 5).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, "iframe[title='recaptcha']"))
            )
            if captcha_frame:
                print("\nATENÇÃO: CAPTCHA detectado! Resolva manualmente para continuar.")
                input("Pressione Enter após resolver o CAPTCHA...")
                return True
        except TimeoutException:
            return False
        except Exception as e:
            print(f"Erro inesperado ao verificar CAPTCHA: {str(e)}")
            return False

    def _click_menu_textual(self):
        try:
            menu_textual_btn = WebDriverWait(self.driver, 10).until(
                EC.element_to_be_clickable((By.LINK_TEXT, "Menu Textual"))
            )
            menu_textual_btn.click()
            print("Botão 'Menu Textual' clicado com sucesso.")

            
            if self._handle_captcha():
                # Re-tenta após CAPTCHA
                self._click_menu_textual()

        except (TimeoutException, NoSuchElementException) as e:
            print(f"Falha ao encontrar/clicar no Menu Textual: {str(e)}")
            raise
        except WebDriverException as e:
            print(f"Erro do WebDriver ao clicar no Menu Textual: {str(e)}")
            raise

    def _click_relacao_processos(self):
        try:
            relacao_btn = WebDriverWait(self.driver, 15).until(
                EC.element_to_be_clickable((By.LINK_TEXT, "Relação de Processos"))
            )
            relacao_btn.click()
            print("Botão 'Relação de Processos' clicado com sucesso.")

            # Verificação adicional de carregamento
            WebDriverWait(self.driver, 2).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, "tr.infraTrClara"))
            )

            # Verifica CAPTCHA após navegação
            if self._handle_captcha():
                self._click_relacao_processos()

        except TimeoutException as e:
            print("Tempo limite excedido aguardando elementos da relação de processos")
            raise
        except Exception as e:
            print(f"Erro inesperado ao acessar relação de processos: {str(e)}")
            raise

    def _get_quantidade_processos(self):
        try:
            caption_element = WebDriverWait(self.driver, 1).until(
                EC.presence_of_element_located((By.CSS_SELECTOR, "caption.infraCaption")))
            caption_text = caption_element.text
            match = re.search(r"\((\d+) registro", caption_text)

            if not match:
                print("Padrão de quantidade não encontrado no texto:", caption_text)
                return "0"

            return match.group(1)

        except (NoSuchElementException, TimeoutException):
            print("Caption não encontrado. Verifique se há processos listados.")
            return "0"
        except Exception as e:
            print(f"Erro ao obter quantidade de processos: {str(e)}")
            return "0"



    def _processar_linhas(self):
        try:
            WebDriverWait(self.driver, 5).until(
                EC.presence_of_element_located((By.ID, 'divInfraAreaTabela'))
            )
    
            soup_filter = SoupStrainer('div', {'id': 'divInfraAreaTabela'})
            soup = BeautifulSoup(self.driver.page_source, 'lxml', parse_only=soup_filter)
            tabela = soup.find('table', class_='infraTable')
    
            if not tabela:
                print("Tabela de processos não encontrada.")
                return
    
            linhas = tabela.find_all('tr', class_=['infraTrClara', 'infraTrEscura'])
    
            for index, row in enumerate(linhas):
                try:
                    data_classe = row.get('data-classe', '')
                    data_competencia = row.get('data-competencia', '')
    
                    colunas = row.find_all('td')
                    if len(colunas) < 11:
                        print(f"Linha {index+1}: Número inválido de colunas ({len(colunas)})")
                        continue
    
                    td_process = colunas[1].get_text(separator="\n").strip().split("\n")
                    numero_processo = td_process[0].strip() if td_process else ""
                    vara = td_process[1].strip() if len(td_process) > 1 else ""
    
                    processo = EprocOverviewEntity(
                        numero_processo=numero_processo,
                        vara=vara,
                        procedimento=colunas[2].get_text(strip=True),
                        parte_ativa=colunas[3].get_text(strip=True),
                        parte_passiva=colunas[4].get_text(strip=True),
                        competencia=colunas[5].get_text(strip=True),
                        assunto=colunas[6].get_text(strip=True),
                        ultima_movimentacao=colunas[7].get_text(strip=True),
                        data_ultima_movimentacao=colunas[8].get_text(strip=True),
                        data_distribuicao=colunas[9].get_text(strip=True),
                        valor_causa=colunas[10].get_text(strip=True),
                        data_classe=data_classe,
                        data_competencia=data_competencia
                    )
    
                    self.lista_processos.append(processo)
    
                except Exception as linha_error:
                    print(f"Erro ao processar linha {index+1}: {str(linha_error)}")
                    continue
    
        except Exception as e:
                    print(f"Erro geral ao processar linhas: {str(e)}")
                    raise


    def execute(self):
        try:
            self._click_menu_textual()
            self._click_relacao_processos()

            qtd = self._get_quantidade_processos()
            print(f"Quantidade de processos encontrados: {qtd}")

            self._processar_linhas()

            print("\n----- Processos encontrados -----")
            for processo in self.lista_processos:
                
                print(processo.numero_processo)

            return self.lista_processos

        except Exception as main_error:
            print(f"Falha crítica na execução: {str(main_error)}")
            self.driver.save_screenshot("error_screenshot.png")
            print("Captura de tela salva como error_screenshot.png")
            return []