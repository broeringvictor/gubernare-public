class TribunalJusticaEprocUrls:
    def __init__(self):
        self.tjsc = "https://eproc1g.tjsc.jus.br/eproc/index.php"
        self.tjsp = "https://esaj.tjsp.jus.br/"
        self.tjrs = "https://www.tjrs.jus.br/"
        self.tjmg = "https://www.tjmg.jus.br/"
        self.tjba = "https://www.tjba.jus.br/"

    def get_url(self, tribunal_code: str) -> str:
        return getattr(self, tribunal_code.lower(), "Tribunal não cadastrado.")
