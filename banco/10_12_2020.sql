-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           8.0.21 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Copiando estrutura do banco de dados para restaurante
DROP DATABASE IF EXISTS `restaurante`;
CREATE DATABASE IF NOT EXISTS `restaurante` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `restaurante`;

-- Copiando estrutura para tabela restaurante.cai_caixa
DROP TABLE IF EXISTS `cai_caixa`;
CREATE TABLE IF NOT EXISTS `cai_caixa` (
  `cai_id` int NOT NULL AUTO_INCREMENT,
  `cai_dthrPagamento` datetime NOT NULL,
  `cai_valorTotal` float NOT NULL,
  `cai_gorjeta` float NOT NULL,
  `cai_disabled` tinyint NOT NULL DEFAULT '0',
  `cai_descricao` varchar(255) NOT NULL,
  `fun_id` int NOT NULL,
  `ped_id` int DEFAULT NULL,
  `com_id` int DEFAULT NULL,
  PRIMARY KEY (`cai_id`),
  KEY `fk_Caixa_Comanda1_idx` (`com_id`),
  KEY `fk_cai_caixa_ped_pedidos1_idx` (`ped_id`),
  KEY `fk_cai_caixa_fun_funcionario1_idx` (`fun_id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.cai_caixa: ~11 rows (aproximadamente)
/*!40000 ALTER TABLE `cai_caixa` DISABLE KEYS */;
INSERT INTO `cai_caixa` (`cai_id`, `cai_dthrPagamento`, `cai_valorTotal`, `cai_gorjeta`, `cai_disabled`, `cai_descricao`, `fun_id`, `ped_id`, `com_id`) VALUES
	(18, '2020-11-29 11:46:55', 53, 0, 0, 'Pagamento de pedido', 1, 70, 87),
	(19, '2020-11-29 11:48:06', 0, 10, 0, 'gorjeta', 1, NULL, NULL),
	(20, '2020-11-26 12:03:23', 10, 0, 0, 'Pagamento de pedido', 1, 71, 88),
	(21, '2020-12-01 12:26:45', 20, 0, 0, 'doce', 1, NULL, NULL),
	(22, '2020-12-03 01:27:21', 6, 0, 0, 'Pagamento de pedido', 1, 75, 92),
	(23, '2020-12-03 01:27:23', 8, 0, 0, 'Pagamento de pedido', 1, 72, 90),
	(24, '2020-12-03 01:27:25', 10, 0, 0, 'Pagamento de pedido', 1, 73, 90),
	(25, '2020-12-03 01:27:28', 10, 0, 0, 'Pagamento de pedido', 1, 74, 91),
	(26, '2020-12-07 15:15:28', 8, 0, 0, 'Pagamento de pedido', 1, 77, 94),
	(27, '2020-12-07 17:47:02', 5, 0, 0, 'Pagamento de pedido', 1, 78, 95),
	(28, '2020-12-07 17:47:37', -10, 0, 0, 'devolução', 1, NULL, NULL),
	(29, '2020-12-08 11:00:00', -10, 0, 0, 'Devolução', 1, NULL, NULL);
/*!40000 ALTER TABLE `cai_caixa` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.coc_comandacancelada
DROP TABLE IF EXISTS `coc_comandacancelada`;
CREATE TABLE IF NOT EXISTS `coc_comandacancelada` (
  `coc_id` int NOT NULL AUTO_INCREMENT,
  `coc_motivo` varchar(255) NOT NULL,
  `coc_dthrCancelada` datetime NOT NULL,
  `com_id` int NOT NULL,
  `fun_id` int NOT NULL,
  PRIMARY KEY (`coc_id`),
  KEY `fk_coc_comandaCancelada_com_comanda1_idx` (`com_id`),
  KEY `fk_coc_comandaCancelada_fun_funcionario1_idx` (`fun_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.coc_comandacancelada: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `coc_comandacancelada` DISABLE KEYS */;
INSERT INTO `coc_comandacancelada` (`coc_id`, `coc_motivo`, `coc_dthrCancelada`, `com_id`, `fun_id`) VALUES
	(1, 'criada por engano', '2020-11-29 22:34:28', 89, 1),
	(2, 'comanda criada com intuito de teste', '2020-12-07 15:14:34', 93, 1);
/*!40000 ALTER TABLE `coc_comandacancelada` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.com_comanda
DROP TABLE IF EXISTS `com_comanda`;
CREATE TABLE IF NOT EXISTS `com_comanda` (
  `com_id` int NOT NULL AUTO_INCREMENT,
  `com_dthrCriacao` datetime NOT NULL,
  `com_foiPaga` tinyint(1) NOT NULL DEFAULT '0',
  `com_foiFinalizada` tinyint(1) NOT NULL DEFAULT '0',
  `com_disabled` tinyint NOT NULL DEFAULT '0',
  `fun_id` int NOT NULL,
  `mes_id` int NOT NULL,
  PRIMARY KEY (`com_id`),
  KEY `fk_Comanda_Funcionario1_idx` (`fun_id`),
  KEY `fk_com_comanda_mes_mesa1_idx` (`mes_id`)
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.com_comanda: ~8 rows (aproximadamente)
/*!40000 ALTER TABLE `com_comanda` DISABLE KEYS */;
INSERT INTO `com_comanda` (`com_id`, `com_dthrCriacao`, `com_foiPaga`, `com_foiFinalizada`, `com_disabled`, `fun_id`, `mes_id`) VALUES
	(87, '2020-10-29 11:43:07', 1, 1, 0, 1, 5),
	(88, '2020-11-29 12:02:02', 1, 1, 0, 1, 7),
	(89, '2020-11-29 22:34:22', 0, 0, 1, 1, 5),
	(90, '2020-11-29 22:43:59', 1, 1, 0, 1, 7),
	(91, '2020-11-30 17:29:03', 1, 1, 0, 1, 11),
	(92, '2020-12-03 01:25:31', 1, 1, 0, 1, 9),
	(93, '2020-12-07 15:13:21', 0, 0, 1, 1, 13),
	(94, '2020-12-07 15:13:40', 1, 1, 0, 1, 10),
	(95, '2020-12-07 17:44:06', 1, 1, 0, 1, 1);
/*!40000 ALTER TABLE `com_comanda` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.fun_funcionario
DROP TABLE IF EXISTS `fun_funcionario`;
CREATE TABLE IF NOT EXISTS `fun_funcionario` (
  `fun_id` int NOT NULL AUTO_INCREMENT,
  `fun_nome` varchar(255) NOT NULL,
  `fun_telefone` varchar(255) DEFAULT NULL,
  `fun_email` varchar(255) NOT NULL,
  `fun_senha` varchar(255) NOT NULL,
  `fun_permissao` int NOT NULL,
  `fun_disabled` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`fun_id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.fun_funcionario: ~5 rows (aproximadamente)
/*!40000 ALTER TABLE `fun_funcionario` DISABLE KEYS */;
INSERT INTO `fun_funcionario` (`fun_id`, `fun_nome`, `fun_telefone`, `fun_email`, `fun_senha`, `fun_permissao`, `fun_disabled`) VALUES
	(1, 'Israel Gonçalves', '1234-1234', 'israel@email.com', '0a47151a074e633ab7b6bed6aab724abbddcd3250f80a06bc612a233a907805101f2441b5b2926e54ce8ac8cfbc074bb7a56748830487df09591dbe167e800f6', 32, 0),
	(2, 'Lojan Eduardo', '12345-6776', 'lojan@email.com', '0a47151a074e633ab7b6bed6aab724abbddcd3250f80a06bc612a233a907805101f2441b5b2926e54ce8ac8cfbc074bb7a56748830487df09591dbe167e800f6', 31, 0),
	(3, 'João Igor', '54321-6789', 'joao@email.com', '0a47151a074e633ab7b6bed6aab724abbddcd3250f80a06bc612a233a907805101f2441b5b2926e54ce8ac8cfbc074bb7a56748830487df09591dbe167e800f6', 23, 0),
	(4, 'Victor Rivera', '', 'victor@email.com', '0a47151a074e633ab7b6bed6aab724abbddcd3250f80a06bc612a233a907805101f2441b5b2926e54ce8ac8cfbc074bb7a56748830487df09591dbe167e800f6', 4, 0),
	(5, 'Vinicius Santos', '', 'vinicius@email.com', '0a47151a074e633ab7b6bed6aab724abbddcd3250f80a06bc612a233a907805101f2441b5b2926e54ce8ac8cfbc074bb7a56748830487df09591dbe167e800f6', 0, 0);
/*!40000 ALTER TABLE `fun_funcionario` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.mes_mesa
DROP TABLE IF EXISTS `mes_mesa`;
CREATE TABLE IF NOT EXISTS `mes_mesa` (
  `mes_id` int NOT NULL AUTO_INCREMENT,
  `mes_identificacao` varchar(50) NOT NULL,
  `mes_disabled` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`mes_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.mes_mesa: ~9 rows (aproximadamente)
/*!40000 ALTER TABLE `mes_mesa` DISABLE KEYS */;
INSERT INTO `mes_mesa` (`mes_id`, `mes_identificacao`, `mes_disabled`) VALUES
	(1, '01', 0),
	(2, '02', 0),
	(3, '03', 0),
	(4, '04', 0),
	(5, '05', 0),
	(6, '06', 0),
	(7, '07', 0),
	(8, '08', 0),
	(9, '09', 0),
	(10, '10', 0),
	(11, '11', 0),
	(12, '12', 0),
	(13, '13', 0);
/*!40000 ALTER TABLE `mes_mesa` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.pec_pedidocancelado
DROP TABLE IF EXISTS `pec_pedidocancelado`;
CREATE TABLE IF NOT EXISTS `pec_pedidocancelado` (
  `pec_id` int NOT NULL AUTO_INCREMENT,
  `pec_motivo` varchar(255) NOT NULL,
  `pec_dthrCancelado` datetime NOT NULL,
  `ped_id` int NOT NULL,
  PRIMARY KEY (`pec_id`),
  KEY `fk_pec_pedidoCancelado_ped_pedidos1_idx` (`ped_id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.pec_pedidocancelado: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `pec_pedidocancelado` DISABLE KEYS */;
/*!40000 ALTER TABLE `pec_pedidocancelado` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.ped_pedidos
DROP TABLE IF EXISTS `ped_pedidos`;
CREATE TABLE IF NOT EXISTS `ped_pedidos` (
  `ped_id` int NOT NULL AUTO_INCREMENT,
  `ped_dthrCriacao` datetime NOT NULL,
  `ped_solicitarPreparo` tinyint(1) NOT NULL DEFAULT '0',
  `ped_foiEntregue` tinyint(1) NOT NULL DEFAULT '0',
  `ped_foiPago` tinyint(1) NOT NULL DEFAULT '0',
  `ped_valor` float NOT NULL,
  `ped_disabled` tinyint NOT NULL DEFAULT '0',
  `com_id` int NOT NULL,
  PRIMARY KEY (`ped_id`),
  KEY `fk_Pedidos_Comanda1_idx` (`com_id`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.ped_pedidos: ~8 rows (aproximadamente)
/*!40000 ALTER TABLE `ped_pedidos` DISABLE KEYS */;
INSERT INTO `ped_pedidos` (`ped_id`, `ped_dthrCriacao`, `ped_solicitarPreparo`, `ped_foiEntregue`, `ped_foiPago`, `ped_valor`, `ped_disabled`, `com_id`) VALUES
	(70, '2020-11-29 11:44:48', 1, 1, 1, 53, 0, 87),
	(71, '2020-11-29 12:02:58', 1, 1, 1, 10, 0, 88),
	(72, '2020-11-29 22:45:08', 1, 1, 1, 8, 0, 90),
	(73, '2020-11-29 22:45:11', 1, 1, 1, 10, 0, 90),
	(74, '2020-11-30 17:36:35', 1, 1, 1, 10, 0, 91),
	(75, '2020-12-03 01:25:47', 1, 1, 1, 6, 0, 92),
	(76, '2020-12-07 15:13:33', 1, 0, 0, 5, 0, 93),
	(77, '2020-12-07 15:13:51', 1, 1, 1, 8, 0, 94),
	(78, '2020-12-07 17:45:19', 1, 1, 1, 5, 0, 95);
/*!40000 ALTER TABLE `ped_pedidos` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.pnp_produtosnopedido
DROP TABLE IF EXISTS `pnp_produtosnopedido`;
CREATE TABLE IF NOT EXISTS `pnp_produtosnopedido` (
  `pnp_id` int NOT NULL AUTO_INCREMENT,
  `pnp_foiFeito` tinyint(1) NOT NULL DEFAULT '0',
  `pnp_dthrCozinha` datetime NOT NULL,
  `pnp_quantidade` int NOT NULL DEFAULT '1',
  `pnp_valor` float NOT NULL,
  `pnp_observacao` varchar(255) NOT NULL DEFAULT 'Nenhuma',
  `pnp_disabled` tinyint NOT NULL DEFAULT '0',
  `ped_id` int NOT NULL,
  `pro_id` int NOT NULL,
  PRIMARY KEY (`pnp_id`),
  KEY `fk_Pedidos_has_Produto_Produto1_idx` (`pro_id`),
  KEY `fk_Pedidos_has_Produto_Pedidos1_idx` (`ped_id`)
) ENGINE=InnoDB AUTO_INCREMENT=140 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.pnp_produtosnopedido: ~16 rows (aproximadamente)
/*!40000 ALTER TABLE `pnp_produtosnopedido` DISABLE KEYS */;
INSERT INTO `pnp_produtosnopedido` (`pnp_id`, `pnp_foiFeito`, `pnp_dthrCozinha`, `pnp_quantidade`, `pnp_valor`, `pnp_observacao`, `pnp_disabled`, `ped_id`, `pro_id`) VALUES
	(123, 1, '2020-11-29 11:45:02', 1, 8, '', 0, 70, 106),
	(124, 1, '2020-11-29 11:45:04', 1, 10, '', 0, 70, 83),
	(125, 1, '2020-11-29 11:45:06', 2, 3.5, 'com ketchup', 0, 70, 78),
	(126, 0, '2020-11-29 11:45:09', 1, 4, '', 2, 70, 99),
	(127, 1, '2020-11-29 11:45:11', 2, 8, '', 0, 70, 5),
	(128, 1, '2020-11-29 11:45:13', 1, 12, '', 0, 70, 89),
	(129, 1, '2020-11-29 12:03:12', 2, 5, '', 0, 71, 94),
	(130, 1, '2020-12-03 01:26:49', 1, 8, '', 0, 72, 106),
	(131, 1, '2020-12-03 01:26:51', 1, 10, '', 0, 73, 83),
	(132, 1, '2020-11-30 17:36:48', 1, 10, '', 0, 74, 83),
	(133, 0, '2020-11-30 17:29:49', 1, 10, '', 1, 74, 83),
	(134, 0, '2020-11-30 17:30:04', 1, 10, '', 1, 74, 83),
	(135, 1, '2020-12-03 01:26:45', 1, 6, '', 0, 75, 94),
	(136, 0, '2020-12-07 15:13:28', 1, 5, '', 0, 76, 94),
	(137, 1, '2020-12-07 15:13:57', 1, 8, '', 0, 77, 7),
	(138, 0, '2020-12-07 17:45:51', 2, 10, '', 2, 78, 83),
	(139, 1, '2020-12-07 17:46:00', 1, 5, 'sem sal', 0, 78, 94);
/*!40000 ALTER TABLE `pnp_produtosnopedido` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.pro_produto
DROP TABLE IF EXISTS `pro_produto`;
CREATE TABLE IF NOT EXISTS `pro_produto` (
  `pro_id` int NOT NULL AUTO_INCREMENT,
  `pro_nome` varchar(255) NOT NULL,
  `pro_valor` float NOT NULL,
  `pro_descricao` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `pro_complemento` tinyint(1) NOT NULL DEFAULT '0',
  `pro_disponivel` tinyint(1) NOT NULL DEFAULT '0',
  `pro_disabled` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`pro_id`)
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.pro_produto: ~40 rows (aproximadamente)
/*!40000 ALTER TABLE `pro_produto` DISABLE KEYS */;
INSERT INTO `pro_produto` (`pro_id`, `pro_nome`, `pro_valor`, `pro_descricao`, `pro_complemento`, `pro_disponivel`, `pro_disabled`) VALUES
	(1, 'Pão de batata', 5, 'Pão com batata', 1, 0, 1),
	(2, 'Salame', 1.5, 'Salame', 1, 0, 0),
	(3, 'Pão com linguiça', 4, 'Contém linguiça', 0, 0, 0),
	(4, 'Arroz com feijão', 10, 'Vegano', 0, 1, 1),
	(5, 'Sanduíche', 8, 'Pão com presunto e queijo', 0, 1, 0),
	(6, 'Batata palha', 3, 'Tradicional, 100g', 1, 1, 0),
	(7, 'Bolo de cenoura', 8, 'Fatia', 0, 1, 0),
	(8, 'Bolo de batata', 120, 'Batata', 0, 1, 1),
	(9, 'Hamburgão', 8, 'Contém queijo e presunto', 0, 1, 1),
	(73, 'Escondidinho de carne', 30, 'Escondidinho feito com batata, carne moída, queijo mussarela, cebola, alho, pimenta e ervas', 0, 1, 0),
	(74, 'Doce de leite Caseiro', 10, 'Doce feito com leite condensado e açúcar cozidos', 0, 1, 1),
	(75, 'Pizza Quatro Queijos', 30, 'Pizza que acompanha quatros tipos de queijo', 0, 1, 0),
	(76, 'Pizza Portuguesa', 35, 'Pizza que acompanha ovo, ervilha, presunto, cebola e queijo', 0, 1, 0),
	(77, 'Torta de Nutella', 8, 'Torta com 180g', 0, 1, 0),
	(78, 'Coxinha', 3.5, 'Coxinha de frango com Catupiry', 0, 0, 0),
	(79, 'Enroladinho de Presunto', 3.5, 'Acompanha presunto e queijo', 0, 1, 0),
	(80, 'X-Egg', 11, 'Pão tradicional, maionese, cebola roxa (ou batata palha), hamburger de picanha 120g, 1 ovo,alho frito e queijo prato', 0, 1, 0),
	(81, 'X-Picanha', 12, 'Pão tradicional, maionese, alface, cebola roxa (ou batata palha), hamburger de picanha 120g, alho frito e queijo prato', 0, 1, 0),
	(82, 'Bife a Milanesa', 12, 'Acompanhamentos arroz, feijão e batata frita', 0, 1, 0),
	(83, 'Coca-Cola 2L', 10, 'Refrigerante', 0, 1, 0),
	(84, 'Coca-Cola 1,5L', 7, 'Refrigerante', 0, 1, 0),
	(85, 'Pepsi', 12, 'Refrigerante 2,5L', 0, 1, 0),
	(86, 'Pepsi', 10, 'Refrigerante 2L', 0, 1, 0),
	(87, 'Frango Empanado', 12, 'Acompanhamentos arroz, feijão e batata frita', 0, 1, 0),
	(88, 'Parmegiana de Carne', 13, 'Acompanhamentos arroz e batata frita', 0, 1, 0),
	(89, 'Hot Dog', 12, 'Pão de hot dog, salsicha, queijo mussarela, queijo parmesão, purê, maionese, batata palha, ketchup, mostarda, alface, tomate e milho', 0, 1, 0),
	(90, 'Hot Dog Frango', 15, 'Pão de hot dog, salsicha, queijo mussarela, queijo parmesão, purê, maionese, batata palha, ketchup, mostarda, alface, tomate, milho e frango', 0, 1, 0),
	(91, 'X-Churrasco', 24, 'Pão, contra-filé, mussarela, parmesão, alface, vinagrete, milho, maionese, purê,batata palha, ketchup, mostarda', 0, 1, 0),
	(92, 'Água Mineral Sem Gás', 2, '500ml', 0, 1, 0),
	(93, 'Suco Del Valle', 5, 'Lata 500ml', 0, 1, 0),
	(94, 'Batata frita', 5, 'Porção pequena, sal opcional', 0, 1, 0),
	(95, 'Torresmo', 7, 'Porção pequena de pele de porco frita', 1, 1, 0),
	(96, 'Farofa', 9, 'Farofa cozida com pedaços de bacon/carne', 1, 1, 0),
	(97, 'Empanadinho de Peixe', 7.5, 'Porção pequena de filé de merluza empanada e frita', 1, 1, 0),
	(98, 'Queijo com presunto', 6.5, 'Porção pequena de queijo e presunto cortados em cubo', 1, 1, 0),
	(99, 'Salada', 4, 'Salada de alface, tomate, cenoura e cebola', 1, 1, 0),
	(100, 'Molho de queijo', 4.5, 'Molho feito com creme de leite, mostarda, queijo coalho e ervas', 1, 1, 0),
	(101, 'Molho russo', 4, 'Molho feito com ketchup, maionese, suco de limão e pimenta', 1, 1, 0),
	(102, 'Molho indiano', 5, 'Molho feito com leite de coco, curry, amido de milho e suco de laranja', 1, 1, 0),
	(103, 'Molho verde de alho', 4, 'Molho feito com creme de leite, salsa, cebolinha e alho', 1, 1, 0),
	(104, 'Coca-Cola 2,5L', 13.5, 'Refrigerante', 0, 0, 0),
	(105, 'Coca-Cola 3L', 15, 'Refrigerante', 0, 0, 0),
	(106, 'Arroz a grega', 8, 'Esqueci', 0, 1, 0);
/*!40000 ALTER TABLE `pro_produto` ENABLE KEYS */;

-- Copiando estrutura para tabela restaurante.rec_reclamacao
DROP TABLE IF EXISTS `rec_reclamacao`;
CREATE TABLE IF NOT EXISTS `rec_reclamacao` (
  `rec_id` int NOT NULL AUTO_INCREMENT,
  `rec_descricao` varchar(255) NOT NULL,
  `rec_categoria` varchar(255) NOT NULL,
  `rec_dthrCriacao` datetime NOT NULL,
  `rec_disabled` tinyint(1) NOT NULL DEFAULT '0',
  `fun_id` int NOT NULL,
  PRIMARY KEY (`rec_id`),
  KEY `fk_rec_reclamacao_fun_funcionario1_idx` (`fun_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela restaurante.rec_reclamacao: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `rec_reclamacao` DISABLE KEYS */;
INSERT INTO `rec_reclamacao` (`rec_id`, `rec_descricao`, `rec_categoria`, `rec_dthrCriacao`, `rec_disabled`, `fun_id`) VALUES
	(9, 'Funcionário trouxe coca-cola quente', 'alimento', '2020-11-29 22:40:37', 1, 1),
	(10, 'demora para entrega mesmo após o preparo na cozinha', 'atendimento', '2020-12-04 19:16:14', 1, 2);
/*!40000 ALTER TABLE `rec_reclamacao` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
