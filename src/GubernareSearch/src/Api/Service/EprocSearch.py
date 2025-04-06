import os
import time
import undetected_chromedriver as uc
from selenium.common.exceptions import TimeoutException, NoSuchElementException
import os
from selenium.webdriver.chrome.options import Options
from src.Domain.Entities.EprocOverviewEntity import EprocOverviewEntity
from .EprocSC import EprocLogin, LegalCasesOverviewActivity, CaptureEvents
from src.Domain.ObjectValues.TribunalJusticaEprocUrls import TribunalJusticaEprocUrls
from undetected_chromedriver import Chrome as uc


def eproc_search():
    try:
        # Configurações de ambiente para TensorFlow
        os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'
        os.environ['TF_ENABLE_XNNPACK'] = '0'

        # Configurações do Chrome
        options = Options()
        options.add_argument("--disable-gpu")
        options.add_argument("--no-sandbox")
        options.add_argument("--disable-dev-shm-usage")
        options.add_argument("--disable-popup-blocking")



        driver = uc(options=options, use_subprocess=True)

        urls = TribunalJusticaEprocUrls()
        driver.get(urls.get_url("tjsc"))

        eproc_login = EprocLogin(driver)
        eproc_login.realizar_login()


        activity = LegalCasesOverviewActivity(driver)
        processos = activity.execute()


        capturador = CaptureEvents(driver, processos)
        resultados = capturador.executar()


        return resultados

    except Exception as e:
        print(f"Erro no fluxo principal: {str(e)}")
        return None


