CREATE TABLE language (
	id integer not null primary key autoincrement,
	name text(100) not null,
	code text(10),
	picture blob
);

CREATE TABLE 'country' (
	id integer not null primary key autoincrement,
	name text(200) not null,
	language_id integer,
	flag blob,
	foreign key (language_id) references language(id)
);

CREATE TABLE director(
	id integer not null primary key autoincrement,
	name text(200) not null,
	country_id integer,
	picture blob,
	foreign key (country_id) references country(id)
);

CREATE TABLE media_type(
	id integer not null primary key autoincrement,
	name text(50) not null,
	symbol blob
);

CREATE TABLE movie(
  id integer not null primary key autoincrement,
  title text(500) not null,
  director_id integer,
  year text(10),
  media_type_id integer not null default 0,
  original_title text(500),
  language_id integer,
  cover blob,
  foreign key (director_id) references director(id),
  foreign key (language_id) references language(id),
  foreign key (media_type_id) references media_type(id)
);

CREATE TABLE 'lend' (
	id integer not null primary key autoincrement,
	movie_id integer not null,
	lent_to text(50) not null,
	lent_date date not null,
	return_date date,
	foreign key (movie_id) references movie(id)
);

CREATE VIEW v_countries AS
	SELECT
		c.id id,
		c.name name,
		c.language_id language_id, lng.name language_name, lng.code language_code
	FROM country c
	LEFT OUTER JOIN language lng ON lng.id = c.language_id;

CREATE VIEW v_directors AS	SELECT
		d.id id,
		d.name name,
		d.country_id, c.name country_name
	FROM director d
	LEFT OUTER JOIN country c ON c.id = d.country_id;

CREATE VIEW v_lends AS
	SELECT
		l.id id,
		m.id movie_id,
		m.title movie_title,
		l.lent_to lent_to,
		l.lent_date lent_date,
		l.return_date return_date,
		(ifnull(julianday(return_date), julianday('now')) - julianday(lent_date)) duration
	FROM movie m
	JOIN lend l ON l.movie_id = m.id;

CREATE VIEW v_movies AS
        SELECT
                m.id id,
                m.title title,
                m.year year,
                m.original_title original_title,
                lng.id language_id, lng.name language_name,
                d.id director_id, d.name director_name,
                c.id country_id, c.name country_name,
                t.id media_type_id, t.name media_type_name,
                l.id lend_id, l.lent_to lent_to,
                m.cover cover
        FROM movie m
        LEFT OUTER JOIN language lng ON lng.id = m.language_id
        LEFT OUTER JOIN director d ON d.id = m.director_id
        LEFT OUTER JOIN country c ON c.id = d.country_id
        LEFT OUTER JOIN media_type t ON t.id = m.media_type_id
        LEFT OUTER JOIN (select id, movie_id, lent_to from lend where return_date is null) l ON l.movie_id = m.id;


INSERT INTO language VALUES(0,'Anglais','en');
INSERT INTO language VALUES(1,'Français','fr');
INSERT INTO language VALUES(2,'Espagnol','es');
INSERT INTO language VALUES(3,'Allemand','de');
INSERT INTO language VALUES(4,'Portuguais','pt');
INSERT INTO language VALUES(5,'Japonais','jp');
INSERT INTO language VALUES(6,'Italien','it');

INSERT INTO country VALUES(0,'Etats-Unis',0,NULL);
INSERT INTO country VALUES(1,'France',1,NULL);
INSERT INTO country VALUES(2,'Royaume-Uni',0,NULL);
INSERT INTO country VALUES(3,'Nouvelle-Zélande',0,NULL);
INSERT INTO country VALUES(4,'Irlande',0,NULL);
INSERT INTO country VALUES(5,'Australie',0,NULL);
INSERT INTO country VALUES(6,'Brésil',4,NULL);
INSERT INTO country VALUES(7,'Japon',5,NULL);
INSERT INTO country VALUES(8,'Allemagne',3,NULL);
INSERT INTO country VALUES(9,'Italie',6,NULL);

INSERT INTO media_type VALUES(0,'DVD',NULL);
INSERT INTO media_type VALUES(1,'DivX',NULL);

