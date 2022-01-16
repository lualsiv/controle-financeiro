USE `controle-financeiro`;

CREATE TABLE Categoria (
    id INT NOT NULL AUTO_INCREMENT,
    nome varchar(255) NOT NULL,
    PRIMARY KEY(id)
)   ENGINE=INNODB;

CREATE TABLE SubCategoria (
    id INT NOT NULL AUTO_INCREMENT,
    nome varchar(255) NOT NULL,
    categoria_id INT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (categoria_id)
      REFERENCES Categoria(id)
)   ENGINE=INNODB;

CREATE TABLE TipoLancamento(
  id INT NOT NULL,
  nome varchar(30) NOT NULL,
  PRIMARY KEY (id)
)ENGINE=INNODB;

CREATE TABLE Lancamento (
    id INT NOT NULL AUTO_INCREMENT,
    valor DECIMAL NOT NULL,
    subcategoria_id INT NOT NULL,
    comentario varchar(255) NOT NULL,
    tipoLancamento INT NOT NULL,
    data DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    FOREIGN KEY (subcategoria_id)
      REFERENCES SubCategoria(id),
    FOREIGN KEY (tipoLancamento)
      REFERENCES TipoLancamento(id)
)   ENGINE=INNODB;


INSERT INTO TipoLancamento(id, nome) VALUES(1,'Credito'),(2,'Debito');

INSERT INTO Categoria(nome ) VALUES('Transporte');
INSERT INTO SubCategoria(nome, categoria_id) VALUES('Combustível',1);

INSERT INTO Categoria(nome ) VALUES('Freelancer');
INSERT INTO SubCategoria(nome, categoria_id) VALUES('Desenvolvimento de SW',2);
INSERT INTO Lancamento(valor, comentario, subcategoria_id, tipoLancamento) VALUES(15,'Autoposto',1,2);
INSERT INTO Lancamento(valor, comentario, subcategoria_id, tipoLancamento) VALUES(15000,'AWS',2,1);