// const API_BASE = "http://localhost:8080"; // Altere se sua porta for diferente
const API_BASE = "https://localhost:7030"; // Altere se sua porta for diferente

let currentPage = 'dashboard';

// Navegação
document.querySelectorAll('.nav-item').forEach(item => {
  item.addEventListener('click', (e) => {
    document.querySelectorAll('.nav-item').forEach(i => i.classList.remove('active'));
    e.target.classList.add('active');
    currentPage = e.target.dataset.page;
    loadPage(currentPage);
  });
});

async function fetchData(endpoint) {
  const response = await fetch(`${API_BASE}${endpoint}`);
  return response.ok ? response.json() : [];
}

async function loadPage(page) {
  const content = document.getElementById('content');
  document.getElementById('page-title').textContent = 
    page === 'dashboard' ? 'Dashboard' : 
    page === 'escolas' ? 'Escolas' : 
    page === 'alunos' ? 'Alunos' : 'Professores';

  if (page === 'dashboard') {
    const [escolas, alunos, professores] = await Promise.all([
      fetchData('/api/Escolas'),
      fetchData('/api/Alunos'),
      fetchData('/api/Professores')
    ]);

    content.innerHTML = `
      <div class="stats">
        <div class="card">
          <h3>Escolas</h3>
          <div class="number">${escolas.length}</div>
        </div>
        <div class="card">
          <h3>Alunos</h3>
          <div class="number">${alunos.length}</div>
        </div>
        <div class="card">
          <h3>Professores</h3>
          <div class="number">${professores.length}</div>
        </div>
      </div>
      <h2>Últimos Cadastros</h2>
      <table><tr><th>Nome</th><th>Tipo</th><th>Escola</th></tr></table>
    `;
  } 
  else if (page === 'alunos') {
    const alunos = await fetchData('/api/Alunos');
    let html = `<h2>Alunos</h2><table><tr><th>Nome</th><th>Email</th><th>Série</th><th>Escola</th></tr>`;
    
    alunos.forEach(a => {
      html += `<tr><td>${a.nome}</td><td>${a.email}</td><td>${a.serie}</td><td>${a.escolaId}</td></tr>`;
    });
    html += `</table>`;
    content.innerHTML = html;
  }
  // Adicione páginas para Escolas e Professores de forma similar...
}

loadPage('dashboard');