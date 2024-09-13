-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           8.1.0 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              12.6.0.6782
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
USE `task_mgmt`;

INSERT INTO `project` (`Id`, `Title`, `Description`, `UpdateAt`, `UserId`, `Enabled`) VALUES
	(1, 'Desafio 1', 'Empresa precisa de sua ajuda para criar um sistema de gerenciamento de tarefas', '2024-09-05 11:48:29', 1, b'1'),
	(2, 'A canção de gelo e fogo', 'Escrito por George R.R. Martin', '2024-09-10 17:52:56', 5, b'1'),
	(3, 'Manutenção jardim', 'Projeto para manutenção do jardim residencial', '2024-09-10 17:53:58', 10, b'1');

-- Copiando dados para a tabela task_mgmt.context_task: ~14 rows (aproximadamente)
INSERT INTO `context_task` (`Id`, `Title`, `Description`, `DueDate`, `Priority`, `Status`, `ProjectId`, `UpdateAt`, `UserId`, `Enabled`) VALUES
	(1, 'Definir SGBD', 'Selecionar o melhor SGBD para o cenário', '2024-10-01 00:00:00', 0, 0, 1, '2024-09-10 18:01:40', 1, b'1'),
	(2, 'Definir Arquitetura', 'Definir Arquitetura da aplicação C#', '2024-10-01 00:00:00', 0, 0, 1, '2024-09-10 18:01:40', 1, b'1'),
	(3, 'Criar Listagem de Projetos', 'listar todos os projetos do usuário', '2024-10-02 00:00:00', 0, 1, 1, '2024-09-10 18:01:40', 1, b'1'),
	(4, 'Visualização de Tarefas', 'visualizar todas as tarefas de um projeto específico', '2024-10-03 00:00:00', 0, 1, 1, '2024-09-10 18:01:40', 1, b'1'),
	(5, 'Criação de Projetos', 'criar um novo projeto', '2024-10-04 00:00:00', 0, 1, 1, '2024-09-10 18:01:40', 1, b'1'),
	(6, 'Criação de Tarefas', 'adicionar uma nova tarefa a um projeto', '2024-10-07 00:00:00', 0, 1, 1, '2024-09-10 18:01:40', 1, b'1'),
	(7, 'Atualização de Tarefas', 'atualizar o status ou detalhes de uma tarefa', '2024-10-08 00:00:00', 0, 2, 1, '2024-09-10 18:01:40', 1, b'1'),
	(8, 'Remoção de Tarefas', 'remover uma tarefa de um projeto', '2024-10-09 00:00:00', 0, 2, 1, '2024-09-10 18:01:40', 1, b'1'),
	
	(9, 'A guerra dos tronos', 'Livro um', '1996-10-01 00:00:00', 0, 2, 2, '2024-09-10 18:01:40', 5, b'1'),
	(10, 'A fúria dos reis', 'Livro dois', '1998-10-01 00:00:00', 1, 2, 2, '2024-09-10 18:01:40', 5, b'1'),
	(11, 'A tormenta de espadas', 'Livro três', '2000-10-01 00:00:00', 2, 2, 2, '2024-09-10 18:01:40', 5, b'1'),
	(12, 'O festim dos corvos', 'Livro quatro', '2005-10-01 00:00:00', 1, 2, 2, '2024-09-10 18:01:40', 5, b'1'),
	(13, 'A dança dos dragões', 'Livro cinco', '2025-10-01 00:00:00', 0, 1, 2, '2024-09-10 18:01:40', 5, b'1'),
	(14, 'Os ventos do inverno', 'Livro seis', '2027-10-01 00:00:00', 0, 0, 2, '2024-09-10 18:01:40', 5, b'1'),
	
	(15, 'Remover pragas', '', '2024-09-14 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(16, 'Cortar grama frente', '', '2024-09-14 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(17, 'Cortar grama fundos', '', '2024-09-15 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(18, 'Podar arecas', '', '2024-09-16 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(19, 'Podar jasmim', '', '2024-09-16 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(20, 'Limpar covas de plantio', '', '2024-09-17 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(21, 'Compostagem nas mudas de frutas', '', '2024-09-17 00:00:00', 0, 0, 3, '2024-09-10 18:01:40', 10, b'1'),
	(22, 'Acionar irrigação', '', '2024-09-18 00:00:00', 0, 0, 3, '2024-09-10 18:00:15', 10, b'1');

-- Copiando dados para a tabela task_mgmt.task_comment: ~4 rows (aproximadamente)
INSERT INTO `task_comment` (`Id`, `ContextTaskId`, `Comment`, `UserId`, `UpdateAt`, `Enabled`) VALUES
	(1, 6, 'Cada tarefa deve ter uma prioridade atribuída (baixa, média, alta).', 2, '2024-09-08 02:28:17', b'1'),
	(2, 6, 'Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.', 2, '2024-09-08 02:28:17', b'1'),
	(3, 5, 'Um projeto não pode ser removido se ainda houver tarefas pendentes associadas a ele.', 2, '2024-09-10 17:38:51', b'1'),
	(4, 5, 'Caso o usuário tente remover um projeto com tarefas pendentes, a API deve retornar um erro e sugerir a conclusão ou remoção das tarefas primeiro.', 2, '2024-09-10 17:38:51', b'1'),
	(5, 7, 'Cada vez que uma tarefa for atualizada (status, detalhes, etc.), a API deve registrar um histórico de alterações para a tarefa.', 2, '2024-09-10 17:38:51', b'1'),
	(6, 7, 'O histórico de alterações deve incluir informações sobre o que foi modificado, a data da modificação e o usuário que fez a modificação.', 2, '2024-09-10 17:38:51', b'1'),
	(7, 4, 'Cada projeto tem um limite máximo de 20 tarefas. Tentar adicionar mais tarefas do que o limite deve resultar em um erro.', 2, '2024-09-10 17:38:51', b'1'),
	(8, 8, 'A API deve fornecer endpoints para gerar relatórios de desempenho, como o número médio de tarefas concluídas por usuário nos últimos 30 dias.', 2, '2024-09-10 17:38:51', b'1');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
