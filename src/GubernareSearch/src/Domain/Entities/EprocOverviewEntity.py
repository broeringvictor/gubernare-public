from dataclasses import dataclass

@dataclass
class EprocOverviewEntity:
    numero_processo: str
    vara: str
    procedimento: str
    parte_ativa: str
    parte_passiva: str
    competencia: str
    assunto: str
    ultima_movimentacao: str
    data_ultima_movimentacao: str
    data_distribuicao: str
    valor_causa: str
    data_classe: str
    data_competencia: str
