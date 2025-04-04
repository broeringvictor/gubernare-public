from dataclasses import dataclass
from datetime import date
from typing import Optional

@dataclass
class LegalCase:
    case_number: str
    client_name: str
    opposing_party: Optional[str] = None
    filing_date: Optional[date] = None
    case_type: Optional[str] = None
    status: Optional[str] = None
    notes: Optional[str] = None
