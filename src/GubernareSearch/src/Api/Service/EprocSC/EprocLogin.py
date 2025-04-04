import os
from dotenv import load_dotenv
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException
import time

load_dotenv()

class EprocLogin:
    def __init__(self, driver):
        self.driver = driver
        self.user = os.getenv("OAB")
        self.password = os.getenv("PASSWORD")  # Remova o 'r' antes da string


    def _handle_error(self, etapa, exception):
        print(f"❌ Erro durante {etapa}: {exception}")

    def realizar_login(self):
        try:
            # Campo de usuário
            try:
                usuario_input = WebDriverWait(self.driver, 15).until(
                    EC.presence_of_element_located((By.ID, "txtUsuario"))
                )
                usuario_input.send_keys(self.user)
                print("✅ Usuário preenchido")
            except Exception as e:
                self._handle_error("preenchimento do usuário", e)

            # Campo de senha
            try:
                senha_input = WebDriverWait(self.driver, 15).until(
                    EC.presence_of_element_located((By.ID, "pwdSenha"))
                )
                senha_input.send_keys(self.password)
                print("✅ Senha preenchida")
            except Exception as e:
                self._handle_error("preenchimento da senha", e)

            # Botão de login
            try:
                btn_entrar = WebDriverWait(self.driver, 15).until(
                    EC.element_to_be_clickable((By.ID, "sbmEntrar"))
                )
                btn_entrar.click()
                time.sleep(1)
                print("✅ Botão de login clicado")
            except Exception as e:
                self._handle_error("clique no botão de login", e)


        except Exception as e:
            self._handle_error("processo de login completo", e)
            return False
