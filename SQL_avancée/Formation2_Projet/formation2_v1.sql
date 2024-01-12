-- 1
DROP TABLE region_cyu
GO
CREATE TABLE region_cyu (
	region_id INT IDENTITY,
	short_name VARCHAR(5),
	full_name VARCHAR(255)
);
GO
-- 2
INSERT INTO region_cyu(short_name, full_name)
VALUES
('ARA', 'Auvergne-Rhône-Alpes'),
('BFC', 'Bourgogne-Franche-Comté'),
('BRE', 'Bretagne'),
('CVL', 'Centre-Val de Loire'),
('COR', 'Corse'),
('GE', 'Grand Est'),
('HDF', 'Hauts-de-France'),
('IDF', 'Île-de-France'),
('NOR', 'Normandie'),
('NAQ', 'Nouvelle-Aquitaine'),
('OCC', 'Occitanie'),
('PDL', 'Pays de la Loire'),
('PACA', 'Provence-Alpes-Côte d''Azur'),
('GLP', 'Guadeloupe'),
('GUF', 'Guyane'),
('MTQ', 'Martinique'),
('REU', 'La Réunion'),
('MYT', 'Mayotte')
GO
-- 3 
DROP TABLE departement_cyu
GO
SELECT *
INTO departement_cyu
FROM departement;
GO
-- 4
ALTER TABLE departement_cyu
ADD region_id INT
GO

UPDATE departement_cyu
SET region_id = 
CASE
	WHEN departement_code IN ('01' , '03', '07', '15', '26', '38', '42', '43', '63', '69', '73', '74')	THEN (select region_id from region_cyu where short_name = 'ARA')
	WHEN departement_code IN ('21', '25', '39', '58', '70', '71', '89', '90')							THEN (select region_id from region_cyu where short_name = 'BFC')
	WHEN departement_code IN ('22', '29', '35', '56')													THEN (select region_id from region_cyu where short_name = 'BRE')
	WHEN departement_code IN ('18', '28', '36', '37', '41', '45')										THEN (select region_id from region_cyu where short_name = 'CVL')
	WHEN departement_code IN ('2b', '2a')																THEN (select region_id from region_cyu where short_name = 'COR')
	WHEN departement_code IN ('08', '10','51', '52', '54', '55', '57', '67', '68', '88')				THEN (select region_id from region_cyu where short_name = 'GE')
	WHEN departement_code IN ('02', '59', '60', '62', '80')												THEN (select region_id from region_cyu where short_name = 'HDF')
	WHEN departement_code IN ('75', '77', '78', '91', '92', '93', '94', '95')							THEN (select region_id from region_cyu where short_name = 'IDF')
	WHEN departement_code IN ('14', '27', '50', '61', '76')												THEN (select region_id from region_cyu where short_name = 'NOR')
	WHEN departement_code IN ('16', '17', '19', '23', '24', '33', '40', '47', '64', '79', '86', '87')	THEN (select region_id from region_cyu where short_name = 'NAQ')
	WHEN departement_code IN ('09', '11', '12', '30', '31', '32', '34', '46', '48', '65', '66', '81', '82') THEN (select region_id from region_cyu where short_name = 'OCC')
	WHEN departement_code IN ('44', '49', '53', '72', '85')												THEN (select region_id from region_cyu where short_name = 'PDL')
	WHEN departement_code IN ('04', '05', '06', '13', '83', '84')										THEN (select region_id from region_cyu where short_name = 'PACA')
	WHEN departement_code IN ('971') THEN (select region_id from region_cyu where short_name = 'GLP')
	WHEN departement_code IN ('973') THEN (select region_id from region_cyu where short_name = 'GUF')
	WHEN departement_code IN ('972') THEN (select region_id from region_cyu where short_name = 'MTQ')
	WHEN departement_code IN ('974') THEN (select region_id from region_cyu where short_name = 'REU')
	WHEN departement_code IN ('976') THEN (select region_id from region_cyu where short_name = 'MYT')
END;
GO


-- Partie 2

---------------
-- Fonctions --
---------------
DROP FUNCTION IF EXISTS dbo.F_cyu_top5_populated
GO
CREATE FUNCTION dbo.F_cyu_top5_populated(@year INT, @departement_code VARCHAR(255))
RETURNS  NVARCHAR(255)
AS
BEGIN
	DECLARE @result NVARCHAR(255)

	SELECT @result = STRING_AGG(ville_nom_reel + ' (' + CONVERT(VARCHAR, ISNULL(ville_population, 0)) + ')' , '; ' ) WITHIN GROUP (ORDER BY ville_population DESC)
	FROM (
		SELECT TOP 5 
				ville_nom_reel as 'ville_nom_reel' ,
				ville_departement,
				CASE 
					WHEN @year = 1999 then ville_population_1999
					WHEN @year = 2010 then ville_population_2010
					WHEN @year = 2012 then ville_population_2012
				END as 'ville_population' 
			FROM villes_france_free 
			WHERE ville_departement = @departement_code
			ORDER BY	
			CASE 
				WHEN @year = 1999 then ville_population_1999
				WHEN @year = 2010 then ville_population_2010
				WHEN @year = 2012 then ville_population_2012
			END DESC
	) top5
	GROUP BY top5.ville_departement

	RETURN @result
END
GO

DROP FUNCTION IF EXISTS dbo.F_cyu_translation
GO
CREATE FUNCTION dbo.F_cyu_translation(@name_input VARCHAR(255))
RETURNS VARCHAR(255)
AS
BEGIN
	DECLARE @name_output NVARCHAR(255)

	RETURN CASE
		WHEN @name_input = 'region_sn'				THEN 'r.short_name' 
		WHEN @name_input = 'departement'			THEN 'd.departement_nom' 
		WHEN @name_input = 'ville_nom_reel'			THEN 'v.ville_nom_reel' 
		WHEN @name_input = 'ville_name_reel'		THEN 'v.ville_nom_reel'
		WHEN @name_input = 'superficie_departement' THEN 'dt.departement_surface' 
		WHEN @name_input = 'superficie_ville'		THEN 'v.ville_surface' 
		WHEN @name_input = 'departement_code'		THEN 'd.departement_code'
		ELSE ''
	END
END
GO

------------------
---- Procedures --
------------------

DROP PROCEDURE IF EXISTS CYU_TAB_1
GO
CREATE PROCEDURE CYU_TAB_1(
	@max_result INT = 0
)
AS -- C:\Users\chenbaoyu\Documents\SQL Server Management Studio
BEGIN
    SET NOCOUNT ON;
	declare @codeError int = 0, @messageError varchar(max) = ''
	IF OBJECT_ID('tempdb..#region_filter') IS NULL
		return -1

	DECLARE @number_citys INT
	SELECT @number_citys = COUNT(*) FROM #region_filter

	SELECT TOP(CASE WHEN @max_result > 0 THEN @max_result ELSE @number_citys END)
				r.full_name as full_name
				, r.short_name as short_name
				, STRING_AGG(d.departement_nom + ' (' + d.departement_code + ')', '; ' ) WITHIN GROUP (ORDER BY d.departement_nom ASC) as list_departement
				, (
					SUBSTRING(
						STRING_AGG(d.departement_nom + ' (' + d.departement_code + ')' , '; ' ) WITHIN GROUP (ORDER BY d.departement_nom ASC)
					, 0, 47)
					+ CASE WHEN LEN(STRING_AGG(d.departement_nom, '; ' )) > 47 THEN ' ...' ELSE '.' END  
				) as list_departement_truncated
	FROM region_cyu r
	JOIN #region_filter rf ON r.short_name = rf.short_name
	LEFT JOIN departement_cyu d 
	ON r.region_id = d.region_id
	GROUP BY r.full_name, r.short_name, r.region_id
END
GO

DROP PROCEDURE IF EXISTS CYU_TAB_2
GO
CREATE PROCEDURE CYU_TAB_2(
	@max_result INT = 0
	--, @count_lines INT = null OUTPUT
)
AS -- C:\Users\chenbaoyu\Documents\SQL Server Management Studio
BEGIN
    SET NOCOUNT ON;
	declare @codeError int = 0, @messageError varchar(max) = ''
	IF OBJECT_ID('tempdb..#departement_filter') IS NULL
		return -1
	
	declare @number_citys int = (SELECT COUNT(1) FROM #departement_filter)
	SELECT TOP(CASE WHEN @max_result > 0 THEN @max_result ELSE @number_citys END)
		d.departement_nom as departement_nom
		,r.short_name as region_name
		,COUNT(DISTINCT v.ville_code_commune) as count_city
		,SUM(v.ville_surface) as departement_surface
		, dbo.F_cyu_top5_populated(1999, d.departement_code) as population_1999 
		, dbo.F_cyu_top5_populated(2010, d.departement_code) as population_2010 
		, dbo.F_cyu_top5_populated(2012, d.departement_code) as population_2012 
	FROM
		departement_cyu as d 
		JOIN #departement_filter df ON d.departement_code = df.departement_code
		JOIN region_cyu as r ON d.region_id = r.region_id
		JOIN villes_france_free as v ON d.departement_code = v.ville_departement
		JOIN #ville_filter as vf ON v.ville_id = vf.ville_id
	GROUP BY d.departement_nom, r.short_name, d.departement_code

END
GO

DROP PROCEDURE IF EXISTS CYU_TAB_3
GO
CREATE PROCEDURE CYU_TAB_3(
	@max_result INT = 0
)
AS -- C:\Users\chenbaoyu\Documents\SQL Server Management Studio
BEGIN
    SET NOCOUNT ON;
	declare @codeError int = 0, @messageError varchar(max) = ''
	IF OBJECT_ID('tempdb..#ville_filter') IS NULL
		return -1

	DECLARE @tmpTable TABLE (nom_reel VARCHAR(100), code_postal VARCHAR(100), info_departement VARCHAR(255))
	DECLARE @number_citys INT
	DECLARE @nom_reel VARCHAR(50), @code_postal VARCHAR(7), @info_departement VARCHAR(50)

	SELECT @number_citys = COUNT(*) FROM #ville_filter

	DECLARE tab3_cursor CURSOR FOR
	SELECT TOP(CASE WHEN @max_result > 0 THEN @max_result ELSE  @number_citys  END)		
		v.ville_nom_reel as nom_reel, 
		v.ville_code_postal as code_postal, 
		CONCAT(d.departement_nom, '(', d.departement_code, ')') as info_departement 
	FROM villes_france_free v
	JOIN #ville_filter vf ON v.ville_id = vf.ville_id
	LEFT JOIN departement_cyu d ON v.ville_departement = d.departement_code

	OPEN tab3_cursor
	FETCH NEXT FROM tab3_cursor INTO @nom_reel, @code_postal, @info_departement

	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO @tmpTable VALUES(@nom_reel, @code_postal, @info_departement)
		FETCH NEXT FROM tab3_cursor INTO  @nom_reel, @code_postal, @info_departement
	END

	CLOSE tab3_cursor
	DEALLOCATE tab3_cursor

	SELECT * FROM @tmpTable
END
GO

DROP PROCEDURE IF EXISTS CYU_MODE_C
GO
CREATE PROCEDURE CYU_MODE_C (
	@tab1_count INT OUTPUT,
	@tab2_count INT OUTPUT,
	@tab3_count INT OUTPUT
)
AS -- C:\Users\chenbaoyu\Documents\SQL Server Management Studio
BEGIN	
	IF OBJECT_ID('tempdb..#region_filter') IS NULL OR OBJECT_ID('tempdb..#departement_filter') IS NULL OR OBJECT_ID('tempdb..#ville_filter') IS NULL
		return -1
	declare @codeError int = 0, @messageError varchar(max) = ''
	SELECT @tab1_count = COUNT(1) FROM #region_filter
									   
	SELECT @tab2_count = COUNT(1) FROM #departement_filter

	SELECT @tab3_count = COUNT(1) FROM #ville_filter
END
GO

---------------------
-- Procedures MAIN --
---------------------
DROP PROCEDURE IF EXISTS CYU_RETRIEVE
GO
CREATE PROCEDURE CYU_RETRIEVE
	@max_result int,
	@json_criteria varchar(max),
	@mode char(1) = 'R'
AS -- C:\Users\chenbaoyu\Documents\SQL Server Management Studio
BEGIN
	SET NOCOUNT ON;
	declare @codeError int = 0, @messageError varchar(max) = ''
	if @max_result < 0
		SELECT @codeError = -1, @messageError = 'The max number of result cannot be negative'
	if @mode NOT IN ('R', 'C')
		SELECT @codeError = -1, @messageError = 'Unknown mode'
	if ISJSON(@json_criteria) <> 1 
		SELECT @codeError = -1, @messageError = 'Unvalid JSON'
	
	if @codeError <> -1
	BEGIN
		SELECT *
		INTO #departement_tmp
		FROM (SELECT 
				d.departement_nom as departement_nom
				,r.short_name as region_name
				,COUNT(DISTINCT v.ville_code_commune) as count_city
				,SUM(v.ville_surface) as departement_surface
			FROM
				departement_cyu as d 
				JOIN region_cyu as r ON d.region_id = r.region_id
				JOIN villes_france_free as v ON d.departement_code = v.ville_departement
			GROUP BY d.departement_nom, r.short_name, departement_code
		) a

		create table #tmp_ville (ville_id int, short_name VARCHAR(5), departement_code VARCHAR(5) )

		declare @query varchar(max) = 
		'	INSERT INTO #tmp_ville(ville_id, short_name, departement_code )
			SELECT v.ville_id, r.short_name, d.departement_code
			FROM villes_france_free v
			LEFT JOIN departement_cyu d ON v.ville_departement = d.departement_code
			LEFT JOIN region_cyu r ON d.region_id = r.region_id
			JOIN #departement_tmp dt ON d.departement_nom = dt.departement_nom
			WHERE
		'

		create table #tmp_critere (nom_critere varchar(255), type_critere varchar(25) null, valeur_critere varchar(max) null)

		
		--insert into #tmp_critere (nom_critere, type_critere, valeur_critere)
		DECLARE @name VARCHAR(30), @type_critere VARCHAR(20), @valeur_critere varchar(20)
		DECLARE critere_curseur CURSOR FOR
		select name , type_critere, valeur_critere
		from OPENJSON (@json_criteria)
		with (	name varchar(max) '$.name',
				type_critere varchar(max) '$.type',
				valeur_critere varchar(max) '$.value')
		OPEN critere_curseur 
		FETCH NEXT FROM critere_curseur INTO @name, @type_critere, @valeur_critere

		WHILE @@FETCH_STATUS = 0
		BEGIN
			if @codeError <> -1
			BEGIN
				if dbo.F_cyu_translation(@name) <> '' 
				BEGIN
					insert into #tmp_critere (nom_critere, type_critere, valeur_critere)
					VALUES (dbo.F_cyu_translation(@name), @type_critere, @valeur_critere)
					FETCH NEXT FROM critere_curseur INTO @name, @type_critere, @valeur_critere
				END
				else
				BEGIN
					SELECT  @codeError = -1, @messageError = 'Unknown column name: ' + @name
					BREAK;
				END
			END
		END
		CLOSE critere_curseur
		DEALLOCATE critere_curseur

		if @codeError <> -1 
		BEGIN
		declare @where varchar(max) = ''
		IF exists (SELECT 1 FROM #tmp_critere WHERE type_critere <> 'multiple')
		BEGIN
			SELECT @where = @where + 
			CASE 
				WHEN @where <> '' THEN ' AND ' ELSE ''
			END
			+ CASE 
				WHEN type_critere = 'like' THEN
					nom_critere + ' LIKE '''	+ valeur_critere + ''' '
				WHEN type_critere = 'compare|inf' THEN
					nom_critere + ' < '		+ valeur_critere
				WHEN type_critere = 'compare|sup' THEN
					nom_critere + ' > '		+ valeur_critere
				ELSE
					''
			END
			FROM #tmp_critere  WHERE type_critere <> 'multiple'
		END
		IF exists (SELECT 1 FROM #tmp_critere WHERE type_critere = 'multiple')
		BEGIN
			DECLARE @multiple VARCHAR(max) = CASE WHEN @where = '' THEN '' ELSE ' AND ' END

			SELECT @multiple = @multiple + CASE WHEN @multiple = '' THEN ' AND ' ELSE '' END + nom_critere + ' IN (' + STRING_AGG('''' + valeur_critere + '''', ',') + ')'
			FROM #tmp_critere where type_critere = 'multiple' GROUP BY nom_critere

			SELECT @where = @where + @multiple
		END

		select @query = @query + @where
		exec(@query)

		select ville_id
		INTO #ville_filter
		FROM #tmp_ville
		select DISTINCT short_name
		INTO #region_filter
		FROM #tmp_ville
		select DISTINCT departement_code
		INTO #departement_filter
		FROM #tmp_ville

		if(@mode = 'R')
		BEGIN
			-- tab1 : (Regions(full_name, short_name, list_departements, list_departements_truncated))
			EXEC CYU_TAB_1 @max_result
			-- tab2 : (Département(full_name, region, count_city, total_surface, top5_city_2012, top5_city_2010, top5_city_1999))
			EXEC CYU_TAB_2 @max_result
			-- tab3 : (Villes(reel_name, code_postal, departement))
			EXEC CYU_TAB_3 @max_result
		END
		if(@mode = 'C')
		BEGIN
			DECLARE @tab1_count INT
			DECLARE @tab2_count INT
			DECLARE @tab3_count INT

			EXEC CYU_MODE_C @tab1_count OUTPUT, @tab2_count OUTPUT, @tab3_count OUTPUT

		
			SELECT @tab1_count as 'Number of lines on tab region'
			SELECT @tab2_count as 'Number of lines on tab department'
			SELECT @tab3_count as 'Number of lines on tab city'
		END
		END
		if @codeError = -1 
			SELECT @codeError, @messageError
	END
END
GO

declare @params_JSON1 varchar(max)	= '[{"name":"region_sn","type":"multiple","value":"PACA"},{"name":"region_sn","type":"multiple","value":"GE"},{"name":"departement", "type":"like", "value":"Al%"},{"name":"ville_nom_reel", "type":"like", "value":"N%"}]'	
declare @params_JSON2 varchar(max)	= '[{"name":"departement_code","type":"multiple","value":"01"},{"name":"departement_code","type":"multiple","value":"02"},{"name":"superficie_departement","type":"compare|sup","value":"1000"}]'
declare @params_JSON3 varchar(max)	= '[{"name":"ville_name_reel","type":"like","value":"Pa%"},{"name":"superficie_ville","type":"compare|inf","value":"6"}]'

--EXEC CYU_RETRIEVE @max_result = 0, @json_criteria = @params_JSON1, @mode = 'R' 
--EXEC CYU_RETRIEVE @max_result = 0, @json_criteria = @params_JSON2, @mode = 'R' 
--EXEC CYU_RETRIEVE @max_result = 0, @json_criteria = @params_JSON3, @mode = 'R' 
--EXEC CYU_RETRIEVE @max_result = 10, @json_criteria = @params_JSON1, @mode = 'R' 
--EXEC CYU_RETRIEVE @max_result = 20, @json_criteria = @params_JSON2, @mode = 'R' 
--EXEC CYU_RETRIEVE @max_result = 30, @json_criteria = @params_JSON3, @mode = 'R' 

EXEC CYU_RETRIEVE @max_result = 30, @json_criteria = @params_JSON1, @mode = 'C' 


