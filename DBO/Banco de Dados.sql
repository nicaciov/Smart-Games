DROP DATABASE IF EXISTS SMART_GAMES;
CREATE DATABASE IF NOT EXISTS SMART_GAMES;
USE SMART_GAMES;

DROP TABLE IF EXISTS games_table;
CREATE TABLE games_table (
  id INT AUTO_INCREMENT PRIMARY KEY,
  game_name VARCHAR(255) NOT NULL,
  game_description VARCHAR(1000),
  game_price DOUBLE NOT NULL,
  inclusion_date DATETIME,
  edit_date DATETIME
);

DROP TABLE IF EXISTS images_table;
CREATE TABLE images_table (
  id INT AUTO_INCREMENT PRIMARY KEY,
  game_id INT NOT NULL,
  photo LONGTEXT,
  inclusion_date DATETIME,
  edit_date DATETIME,
  FOREIGN KEY (game_id) REFERENCES games_table(id) ON DELETE CASCADE
);

DROP TABLE IF EXISTS platforms_table;
CREATE TABLE platforms_table (
  id INT AUTO_INCREMENT PRIMARY KEY,
  game_id INT NOT NULL,
  platform VARCHAR(100),
  inclusion_date DATETIME,
  edit_date DATETIME,
  FOREIGN KEY (game_id) REFERENCES games_table(id) ON DELETE CASCADE
);

DROP TABLE IF EXISTS stores_table;
CREATE TABLE stores_table (
  id INT AUTO_INCREMENT PRIMARY KEY,
  game_id INT NOT NULL,
  store_id INT,
  inclusion_date DATETIME,
  edit_date DATETIME,
  FOREIGN KEY (game_id) REFERENCES games_table(id) ON DELETE CASCADE
);

DROP TABLE IF EXISTS place_stores_table;
CREATE TABLE place_stores_table (
  id INT AUTO_INCREMENT PRIMARY KEY, 
  store VARCHAR(100),
  lat VARCHAR(100),
  lon VARCHAR(100),
  inclusion_date DATETIME,
  edit_date DATETIME
);

DROP TABLE IF EXISTS shop_cart;
CREATE TABLE shop_cart (
  id INT AUTO_INCREMENT PRIMARY KEY,
  game_id INT NOT NULL, 
  inclusion_date DATETIME,
  edit_date DATETIME,
  FOREIGN KEY (game_id) REFERENCES games_table(id) ON DELETE CASCADE
);

DROP TABLE IF EXISTS check_out;
CREATE TABLE check_out (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    family_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    cpf VARCHAR(14) NOT NULL,
    address VARCHAR(255) NOT NULL,
    address_complement VARCHAR(255),
    country VARCHAR(255) NOT NULL,
    state VARCHAR(255) NOT NULL,
    cep VARCHAR(10) NOT NULL,
    type_payment_credit VARCHAR(50),
    type_payment_debit VARCHAR(50),
    type_payment_paypal VARCHAR(50),
    card_name VARCHAR(255),
    card_number VARCHAR(16),
    card_date_expiration VARCHAR(7),
    card_cvv VARCHAR(4),
    code VARCHAR(255),
    inclusion_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    edit_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
 
DROP TABLE IF EXISTS checkout_shop_itens;
CREATE TABLE checkout_shop_itens (
  id INT AUTO_INCREMENT PRIMARY KEY,
  game_id INT NOT NULL, 
  check_out_id INT NOT NULL, 
  inclusion_date DATETIME,
  edit_date DATETIME,
  FOREIGN KEY (game_id) REFERENCES games_table(id) ON DELETE CASCADE,
  FOREIGN KEY (check_out_id) REFERENCES check_out(id) ON DELETE CASCADE
);


INSERT INTO place_stores_table (store, lat, lon, inclusion_date, edit_date) 
	VALUES ('Loja Tamboré', '-23.504245112921097', '-46.83435060312886', Now(), Now()),
		   ('Loja União', '-23.541282861784126', '-46.76638890860699', Now(), Now()),
		   ('Loja Iguatemi', '-23.504386186165057', '-46.84833905906179', Now(), Now());

INSERT INTO games_table (game_name, game_description, game_price, inclusion_date, edit_date) 
VALUES ('Overwatch', 'Overwatch é um jogo de tiro em equipe que conta com um elenco diversificado de heróis poderosíssimos. Viaje pelo mundo, monte uma equipe e dispute objetivos em combates 6v6 de tirar o fôlego.', 
			160.00, Now(), Now()),
	   ('Spider-Man', 'Spider-Man é um jogo eletrônico de ação-aventura desenvolvido pela Insomniac Games e publicado pela Sony Interactive Entertainment. É baseado nos personagens, mitologia e adaptações em outras mídias do super-herói de histórias em quadrinhos Homem-Aranha da Marvel Comics, tendo sido lançado exclusivamente para PlayStation 4 em 7 de setembro de 2018. Na história, o criminoso super-humano Senhor Negativo organiza um plano para se vingar do prefeito Norman Osborn e assumir o controle do submundo criminal de Nova Iorque. O Homem-Aranha precisa proteger a cidade assim que o Senhor Negativo ameaça lançar um vírus mortal por toda a área, ao mesmo tempo que é forçado a lidar com seus problemas pessoais como Peter Parker.', 
			116.90, Now(), Now()),
	   ('God Of War', 'É um novo começo para Kratos. Vivendo como um homem longe da sombra dos deuses, ele se aventura pelas selvagens florestas nórdicas com seu filho Atreus, lutando para cumprir uma missão profundamente pessoal. O Santa Monica Studio e o diretor de criação Cory Barlog lançam um novo começo para um dos personagens mais conhecidos dos jogos. Vivendo como um homem, fora da sombra dos deuses, Kratos deve se adaptar a terras desconhecidas, ameaças inesperadas e a uma segunda oportunidade de ser pai. Junto ao seu filho, Atreus, os dois vão se aventurar pelas selvagens florestas nórdicas e lutar para cumprir uma missão profundamente pessoal.', 
			69.90, Now(), Now()),
       ('Ghost of Tsushima', 'No final do século XIII, o império mongol devastou nações inteiras durante sua campanha para conquistar o Oriente. A Ilha de Tsushima é tudo o que está entre o Japão continental e uma enorme frota invasora mongol comandada pelo implacável e sagaz general Khotun Khan. À medida que a ilha queima na esteira da primeira onda do assalto mongol, o guerreiro samurai Jin Sakai mantém-se como um dos último membros sobreviventes de seu clã. Ele está decidido a proteger seu povo e recuperar seu lar, independente do que aconteça, custe o que custar. Será preciso romper com as tradições que o tornaram um guerreiro para forjar um novo caminho do Fantasma e declarar uma guerra incomum pela liberdade de Tsushima.', 
			249.00, Now(), Now()),
       ('Tom Clancys Splinter Cell: Double Agent', 'Vivencie a tensão incessante e os dilemas da vida de um agente duplo. Minta. Mate. Prejudique. Traia. Tudo para proteger os inocentes. Até onde você iria para ganhar a confiança do inimigo? Como o agente secreto Sam Fisher, você deve se infiltrar num grupo terrorista cruel e destruí-lo internamente. Você precisará ponderar muito bem as consequências dos seus atos. Mate terroristas demais e estragará seu disfarce. Hesite e milhões morrerão. Faça o que for preciso para completar a missão, mas procure sobreviver.', 
			36.00, Now(), Now()),
       ('God of War III', 'Kratos está devolta, ainda com a vingança pulsando forte em suas veias. O Olimpo e seus deuses é o seu alvo, não importa o preço que o Deus da Guerra irá pagar. Neste terceiro e épico capítulo você irá controlar Kratos através de batalhas celestiais contra os deuses mais poderosos de todo o Olimpo – inclusive voltará ao inferno e acertar as contas com seu pai colossal e poderoso, Kronos. Deuses como Hermes e Hades não serão páreo para seu poder, os olhos ardentes daquele que busca vingança que nem a mais cruel das mortes pode deter. Enfrente desafios arrasadores e enfrente monstros poderosos como a Quimera, o Cérberus e muitos outros seres místicos que estão ali apenas para acabar com sua existência. Não pare por nada até enfrentar o deus dos deuses: Zeus, e acabar com isso de uma vez por todas.', 
			50.00, Now(), Now()),
       ('Watch Dogs 2', 'Junte-se ao Dedsec, um grupo notório de hackers, para executar o maior hack da história; Derrube o ctOS 2.0, um sistema operacional invasivo que está sendo usado por um gênio do crime para monitorar e manipular os cidadãos em uma escala massiva.', 
			60.00, Now(), Now()),
       ('Batman arkham city', 'Batman: Arkham City é um jogo eletrônico de Ação-Aventura e Stealth, baseado na série de quadrinhos Batman da DC Comics. O jogo é distribuído para: PlayStation 3, Xbox 360 e Microsoft Windows. Foi desenvolvido pela Rocksteady Studios e foi publicado pela Warner Bros. Interactive Entertainment e DC Entertainment.', 
			31.00, Now(), Now());

INSERT INTO platforms_table  (game_id, platform, inclusion_date, edit_date) 
VALUES (1, 'PC', Now(), Now()),
	   (1, 'PS4', Now(), Now()),
	   (1, 'XBox One', Now(), Now()),
	   (1, 'Switch', Now(), Now()),
       (2, 'PS4', Now(), Now()),
       (3, 'PS4', Now(), Now()),
       (4, 'PS4', Now(), Now()),
       (5, 'XBox', Now(), Now()),
       (5, 'PS2', Now(), Now()),
       (5, 'PS3', Now(), Now()),
       (5, 'PC', Now(), Now()),
       (6, 'PS3', Now(), Now()),
       (6, 'PS4', Now(), Now()),
       (7, 'PC', Now(), Now()),
       (7, 'PS4', Now(), Now()),
       (7, 'XBox One', Now(), Now()),
       (8, 'XBox', Now(), Now()),
       (8, 'PC', Now(), Now()),
       (8, 'WiiU', Now(), Now());
 
INSERT INTO stores_table  (game_id, store_id, inclusion_date, edit_date) 
VALUES (1, 1, Now(), Now()),
	   (1, 2, Now(), Now()),
	   (2, 1, Now(), Now()),
	   (2, 2, Now(), Now()),
	   (2, 3, Now(), Now()),
	   (3, 1, Now(), Now()),
	   (3, 2, Now(), Now()),
	   (4, 3, Now(), Now()), 
	   (4, 2, Now(), Now()),
	   (5, 1, Now(), Now()),
	   (5, 3, Now(), Now()),
	   (5, 2, Now(), Now()),
	   (6, 3, Now(), Now()), 
	   (6, 2, Now(), Now()),
	   (7, 1, Now(), Now()),
	   (7, 2, Now(), Now()),
       (8, 1, Now(), Now()),
	   (8, 2, Now(), Now()),
	   (8, 3, Now(), Now());
 
 /*
SELECT * FROM games_table AS j 
	INNER JOIN images_table AS i ON j.id = i.game_id
	INNER JOIN platforms_table AS p ON p.game_id = i.game_id
	INNER JOIN stores_table AS l ON l.game_id = i.game_id; 

SELECT * FROM games_table;
SELECT * FROM images_table;
SELECT id, game_id, platform, inclusion_date, edit_date FROM platforms_table;
SELECT id, game_id, store, inclusion_date, edit_date FROM stores_table;


SELECT s.id, game_id, store, lat, lon, s.inclusion_date, s.edit_date FROM stores_table AS s INNER JOIN place_stores_table AS p ON p.id = s.store_id WHERE game_id = 1;

SELECT s.id, game_id, store, s.inclusion_date, s.edit_date FROM stores_table AS s INNER JOIN place_stores_table AS p ON p.id = s.store_id;

SELECT * FROM place_stores_table;
SELECT * FROM stores_table;

SELECT * FROM shop_cart;
SELECT * FROM check_out;
SELECT * FROM checkout_shop_itens; 

*/
 