import os
import time
import undetected_chromedriver as uc
from selenium.common.exceptions import TimeoutException, NoSuchElementException

# Para trabalhar com as ChromeOptions
from selenium.webdriver.chrome.options import Options
from src.Domain.Entities.EprocOverviewEntity import EprocOverviewEntity

# Supondo que estas importações funcionem no seu projeto
from .EprocSC import EprocLogin, LegalCasesOverviewActivity, CaptureEvents
from src.Domain.ObjectValues.TribunalJusticaEprocUrls import TribunalJusticaEprocUrls

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'
os.environ['TF_ENABLE_XNNPACK'] = '0'

# Configurações do Chrome
options = Options()
options.add_argument("--disable-gpu")
options.add_argument("--no-sandbox")
options.add_argument("--disable-dev-shm-usage")
options.add_argument("--disable-popup-blocking")
# options.add_argument("--headless")
# Inicializa o undetected_chromedriver com as opções definidas
driver = uc.Chrome(options=options, use_subprocess=True)


urls = TribunalJusticaEprocUrls()
driver.get(urls.get_url("tjsc"))


eproc_login = EprocLogin(driver)
eproc_login.realizar_login()


activity = LegalCasesOverviewActivity(driver)

processos = activity.execute()
capture = CaptureEvents(driver, processos)
resultado_captura = capture.execute()



resultados = []
for resultado in resultados:
    print(f"Processo: {resultado.numero_processo}")
    print(f"Eventos: {resultado.eventos}")
    print(f"Data do último evento: {resultado.data_ultimo_evento}")
    print("-" * 40)


while True:
    user_input = input('Digite "m" para fechar o navegador: ').strip().lower()
    if user_input == "m":
        print("Fechando o navegador...")
        driver.quit()
        break
    else:
        print('Comando não reconhecido. Digite "m" para sair.')