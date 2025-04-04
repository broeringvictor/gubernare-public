from dataclasses import dataclass
from datetime import datetime
import re

from selenium.webdriver.common.by import By


@dataclass
class EventoEntity:
    numero_evento: str
    data_hora: datetime
    descricao: str
    usuario: str
    documentos: list[str]
    tipo: str = "GERAL"
    parte_relacionada: str = None
    prazo_status: str = None
    prazo_data_inicio: datetime = None
    prazo_data_fim: datetime = None
    prazo_detalhes: str = None

    @classmethod
    def from_tr_element(cls, tr_element):
        """Constrói o objeto a partir de um elemento <tr> da tabela de eventos"""
        tds = tr_element.find_elements(By.TAG_NAME, "td")

        # Extração básica
        numero_evento = tds[0].text.strip().split()[0]
        data_str = tds[1].text.strip()
        descricao = tr_element.find_element(By.CLASS_NAME, "infraEventoDescricao").text
        usuario = tr_element.find_element(By.CLASS_NAME, "infraEventoUsuario").get_attribute("aria-label").split("<br>")[0]

        # Documentos
        documentos = [doc.text for doc in tr_element.find_elements(By.CLASS_NAME, "infraLinkDocumento")]

        # Cria objeto base
        evento = cls(
            numero_evento=numero_evento,
            data_hora=cls._parse_date(data_str),
            descricao=descricao,
            usuario=usuario,
            documentos=documentos
        )

        # Detecção e tratamento de prazos
        if "infraEventoPrazo" in tr_element.get_attribute("class"):
            evento.tipo = "PRAZO"
            evento._extrair_dados_prazo(tr_element)

        return evento

    def _extrair_dados_prazo(self, tr_element):
        """Extrai informações específicas de prazos"""
        descricao_completa = tr_element.find_element(By.CLASS_NAME, "infraEventoDescricao").text

        # Extração de datas do prazo
        padrao_data = r"\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2}"
        datas = re.findall(padrao_data, descricao_completa)

        self.prazo_data_inicio = self._parse_date(datas[0]) if len(datas) > 0 else None
        self.prazo_data_fim = self._parse_date(datas[1]) if len(datas) > 1 else None

        # Status do prazo
        if "infraEventoPrazoFechado" in tr_element.get_attribute("class"):
            self.prazo_status = "FECHADO"
        elif "infraEventoPrazoAberto" in tr_element.get_attribute("class"):
            self.prazo_status = "ABERTO"

        # Parte relacionada
        parte_element = tr_element.find_elements(By.CLASS_NAME, "infraEventoPrazoParte")
        self.parte_relacionada = parte_element[0].text if parte_element else None

    @staticmethod
    def _parse_date(date_str):
        """Converte strings de data no formato dd/mm/yyyy HH:mm:ss para datetime"""
        try:
            return datetime.strptime(date_str, "%d/%m/%Y %H:%M:%S")
        except:
            return None