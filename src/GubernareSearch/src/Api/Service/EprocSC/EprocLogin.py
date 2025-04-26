from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException
import time

class EprocLogin:
    def __init__(self, driver, login, password):
        self.driver = driver
        self.user = login  # Credencial recebida dinamicamente
        self.password = password  # Credencial recebida dinamicamente

    def _handle_error(self, etapa, exception):
        print(f"❌ Erro durante {etapa}: {exception}")
        return False

    def realizar_login(self):
        try:
            # Preenchimento do usuário
            usuario_input = WebDriverWait(self.driver, 15).until(
                EC.presence_of_element_located((By.ID, "txtUsuario"))
            )
            usuario_input.send_keys(self.user)
            print("✅ Usuário preenchido")

            # Preenchimento da senha
            senha_input = WebDriverWait(self.driver, 15).until(
                EC.presence_of_element_located((By.ID, "pwdSenha"))
            )
            senha_input.send_keys(self.password)
            print("✅ Senha preenchida")

            # Clique no botão de login
            btn_entrar = WebDriverWait(self.driver, 15).until(
                EC.element_to_be_clickable((By.ID, "sbmEntrar"))
            )
            btn_entrar.click()
            time.sleep(1)
            print("✅ Botão de login clicado")

            # Verificação de login bem-sucedido
            WebDriverWait(self.driver, 15).until(
                EC.presence_of_element_located((By.XPATH, '//*[@id="divInfraBarraLocalizacao"]/div/h1'))
            )
            print("✅ Login realizado com sucesso")
            return True

        except TimeoutException as e:
            print("❌ Tempo de espera excedido durante o login")
            return self._handle_error("timeout do login", e)
        except Exception as e:
            return self._handle_error("processo de login completo", e)