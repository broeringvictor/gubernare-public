import os
import time
import undetected_chromedriver as uc
from jupyter_events.cli import console

from selenium.webdriver.chrome.options import Options

from .EprocSC import EprocLogin, LegalCasesOverviewActivity, CaptureEvents
from src.Domain.ObjectValues.TribunalJusticaEprocUrls import TribunalJusticaEprocUrls

def eproc_search(login, password):
    try:
        options = uc.ChromeOptions()
      
        options.add_argument("--disable-blink-features=AutomationControlled")
        driver = uc.Chrome(options=options, use_subprocess=True)
    
        # Configurações de ambiente
        os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'
        os.environ['TF_ENABLE_XNNPACK'] = '0'

        # Configurações do navegador
        options = Options()
        options.add_argument("--disable-gpu")
        options.add_argument("--no-sandbox")
        options.add_argument("--disable-dev-shm-usage")
        options.add_argument("--disable-popup-blocking")

        
        urls = TribunalJusticaEprocUrls()
        driver.get(urls.get_url("tjsc"))

        # Realiza login com credenciais dinâmicas
        eproc_login = EprocLogin(driver, login, password)
        if not eproc_login.realizar_login():
            return None

        # Captura processos
        activity = LegalCasesOverviewActivity(driver)
        processos = activity.execute()

        # Captura eventos
        capturador = CaptureEvents(driver, processos)
        resultados = capturador.executar()

        return resultados

    except Exception as e:
        print(f"Erro no fluxo principal: {str(e)}")
        return None
    finally:
        if 'driver' in locals():
            console.log("Fechando navegador...")