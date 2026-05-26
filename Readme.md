


# INSERTs Completo (SQL Server)
-- =============================================
-- INSERIR ESCOLAS primeiro
-- =============================================

INSERT INTO Escolas (Nome, Endereco, Cidade, Telefone) VALUES 
('Escola Piaget', 'Rua das Flores, 123', 'São Paulo', '(11) 98765-4321'),
('Colégio Einstein', 'Av. Paulista, 1500', 'São Paulo', '(11) 3456-7890'),
('Instituto Montessori', 'Rua das Acácias, 450', 'Campinas', '(19) 98765-1234');

-- =============================================
-- INSERIR ALUNOS
-- =============================================

INSERT INTO Alunos (Nome, Email, DataNascimento, Serie, EscolaId) VALUES 
('João Silva', 'joao.silva@email.com', '2015-05-12', '6º Ano', 1),
('Maria Oliveira', 'maria.oliveira@email.com', '2014-08-25', '7º Ano', 1),
('Pedro Santos', 'pedro.santos@email.com', '2016-01-10', '5º Ano', 1),
('Ana Clara Mendes', 'ana.mendes@email.com', '2013-11-30', '8º Ano', 2),
('Lucas Ferreira', 'lucas.ferreira@email.com', '2015-03-18', '6º Ano', 2);

-- =============================================
-- INSERIR PROFESSORES
-- =============================================

INSERT INTO Professores (Nome, Email, Disciplina, Titulacao, EscolaId) VALUES 
('Prof. Carlos Almeida', 'carlos.almeida@escola.com', 'Matemática', 'Mestre', 1),
('Profª Juliana Costa', 'juliana.costa@escola.com', 'Português', 'Doutora', 1),
('Prof. Roberto Mendes', 'roberto.mendes@escola.com', 'História', 'Especialista', 1),
('Profª Fernanda Lima', 'fernanda.lima@escola.com', 'Ciências', 'Mestre', 2),
('Prof. Marcos Silva', 'marcos.silva@escola.com', 'Inglês', 'Especialista', 2);







# Verificar os dados inseridos:

-- Ver tudo
SELECT * FROM Escolas;
SELECT * FROM Alunos;
SELECT * FROM Professores;

-- Ver com relacionamentos
SELECT 
    e.Nome AS Escola,
    a.Nome AS Aluno,
    a.Serie
FROM Escolas e
LEFT JOIN Alunos a ON a.EscolaId = e.Id;

SELECT 
    e.Nome AS Escola,
    p.Nome AS Professor,
    p.Disciplina
FROM Escolas e
LEFT JOIN Professores p ON p.EscolaId = e.Id;





