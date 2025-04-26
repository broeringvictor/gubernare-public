from flask import Flask, request, jsonify
from flask_caching import Cache
from flask_cors import CORS
import hashlib

# Remova a importação incorreta de await_promise
from src.Api.Service.EprocSearch import eproc_search

app = Flask(__name__)
CORS(app)

app.config['CACHE_TYPE'] = 'simple'
app.config['CACHE_DEFAULT_TIMEOUT'] = 80000
cache = Cache(app)

def get_cached_processes(login, password):
    key = hashlib.sha256(f"{login}:{password}".encode()).hexdigest()
    processes = cache.get(key)

    if not processes:
        processes = eproc_search(login, password)
        if processes:
            cache.set(key, processes)

    return processes

@app.route("/alllegalproceeding", methods=['POST'])
def all_legal_proceeding():
    data = request.get_json()

    if not data:
        return jsonify({'error': 'Request body deve ser JSON'}), 400

    login = data.get('login')
    password = data.get('password')

    if not login or not password:
        return jsonify({'error': 'Parâmetros login e password são obrigatórios'}), 400

    try:
        # Use a função de cache
        processes = get_cached_processes(login, password)

        if processes is None:
            return jsonify({'error': 'Falha ao obter processos'}), 500

        # Retorne os processos diretamente com jsonify
        return jsonify(processes)

    except Exception as e:
        app.logger.error(f'Erro no endpoint /alllegalproceeding: {str(e)}')
        return jsonify({'error': 'Erro interno no servidor'}), 500

if __name__ == "__main__":
    app.run(debug=True)