from flask import Flask, request, jsonify
from flask_caching import Cache
from datetime import datetime
import re
from src.Api.Service.EprocSearch import eproc_search

app = Flask(__name__)

# Configuração do cache
app.config['CACHE_TYPE'] = 'SimpleCache'
app.config['CACHE_DEFAULT_TIMEOUT'] = 80000
cache = Cache(app)

def get_cached_processes():
    # Verifica se já existe no cache
    processes = cache.get('all_processes')

    if not processes:
        # Se não existir, busca e armazena no cache
        processes = eproc_search()
        cache.set('all_processes', processes)

    return processes
@app.route("/allprocesses", methods=['GET'])
def all_processes():
    processes = get_cached_processes()
    return jsonify(processes), 200

@app.route("/processes_with_deadline", methods=['GET'])
@cache.memoize(timeout=60)  # Cache baseado nos parâmetros
def processes_with_deadline():
    date_param = request.args.get('date')
    if not date_param:
        return jsonify({'error': 'Parâmetro "date" é obrigatório'}), 400

    try:
        input_date = datetime.strptime(date_param, '%d%m%Y').date()
    except ValueError:
        return jsonify({'error': 'Formato de data inválido. Use DDMMYYYY'}), 400

    processes = get_cached_processes()
    filtered_processes = []

    for process in processes:
        eventos = process.get('eventos', [])
        eventos_prazo = []

        for evento in eventos:
            if len(evento) < 3:
                continue

            descricao = evento[2]
            data_final = None

            for linha in descricao.split('\n'):
                match = re.search(r'Data final:\s*(\d{2}/\d{2}/\d{4})', linha)
                if match:
                    try:
                        data_final = datetime.strptime(match.group(1), '%d/%m/%Y').date()
                        break
                    except ValueError:
                        continue

            if data_final and data_final > input_date:
                eventos_prazo.append({
                    'id_evento': evento[0],
                    'data_final': match.group(1),
                    'descricao': descricao
                })

        if eventos_prazo:
            filtered_processes.append({
                'numero_processo': process.get('numero_processo'),
                'eventos_prazo': eventos_prazo
            })

    return jsonify(filtered_processes), 200

if __name__ == "__main__":
    app.run(debug=True)