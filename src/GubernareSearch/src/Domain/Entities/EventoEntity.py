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
    prazo_status: str = None
    prazo_data_inicio: datetime = None
    prazo_data_fim: datetime = None

    @classmethod
    def from_tr_element(cls, tr_element):
        # Obtém todas as células (td) da linha
        tds = tr_element.find_elements(By.TAG_NAME, "td")
        
        # Número do evento: assume que vem na primeira célula
        numero_evento = tds[0].text.split()[0].strip()
        
        # Data do evento: assume que está na segunda célula no formato "dd/mm/yyyy HH:mm:ss"
        data_str = tds[1].text.strip()
        event_date = cls._parse_date(data_str)
        
        # Descrição: pega o texto da terceira célula
        descricao = tds[2].text.strip()
        
        # Usuário: tenta pegar o primeiro valor do atributo aria-label do elemento com a classe "infraEventoUsuario"
        try:
            usuario_elem = tr_element.find_element(By.CLASS_NAME, "infraEventoUsuario")
            usuario_attr = usuario_elem.get_attribute("aria-label")
            usuario = usuario_attr.split("<br>")[0].strip() if usuario_attr else usuario_elem.text.strip()
        except Exception:
            usuario = ""
        
        # Documentos: extrai os textos dos links (se houver)
        documentos = []
        try:
            doc_links = tr_element.find_elements(By.CLASS_NAME, "infraLinkDocumento")
            documentos = [doc.text.strip() for doc in doc_links if doc.text.strip()]
        except Exception:
            pass

        # Se na descrição existir informação de prazo, extrai as datas e status
        prazo_data_inicio = None
        prazo_data_fim = None
        prazo_status = None
        if "Data inicial da contagem do prazo:" in descricao:
            inicio_match = re.search(r"Data inicial da contagem do prazo:\s*([\d/]+\s*[\d:]+)", descricao)
            final_match = re.search(r"Data final:\s*([\d/]+\s*[\d:]+)", descricao)
            status_match = re.search(r"Status:([A-Z]+)", descricao)
            if inicio_match:
                prazo_data_inicio = cls._parse_date(inicio_match.group(1))
            if final_match:
                prazo_data_fim = cls._parse_date(final_match.group(1))
            if status_match:
                prazo_status = status_match.group(1).strip()

        tipo = "PRAZO" if prazo_data_inicio or prazo_data_fim else "GERAL"

        return cls(
            numero_evento=numero_evento,
            data_hora=event_date,
            descricao=descricao,
            usuario=usuario,
            documentos=documentos,
            tipo=tipo,
            prazo_status=prazo_status,
            prazo_data_inicio=prazo_data_inicio,
            prazo_data_fim=prazo_data_fim
        )

    @staticmethod
    def _parse_date(date_str):
        """Converte uma string no formato 'dd/mm/yyyy HH:mm:ss' para um objeto datetime.
           Retorna None se a conversão falhar."""
        try:
            return datetime.strptime(date_str, "%d/%m/%Y %H:%M:%S")
        except Exception:
            return None
