PGDMP     1    3            	    {            Reto1    15.3    15.3 P    \           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ]           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ^           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            _           1262    41091    Reto1    DATABASE     z   CREATE DATABASE "Reto1" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE "Reto1";
                postgres    false            �            1255    57839 +   consultar_asignaturas_por_profesor(integer)    FUNCTION     �  CREATE FUNCTION public.consultar_asignaturas_por_profesor(id_profesor integer) RETURNS TABLE(nombre_asignatura character varying, nombre_grupo character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        a.nombre AS nombre_asignatura,
        pa.nombre_grupo
    FROM profesor_asignatura pa
    INNER JOIN asignaturas a ON pa.idasignatura = a.id
    WHERE pa.idprofesor = id_profesor;
END;
$$;
 N   DROP FUNCTION public.consultar_asignaturas_por_profesor(id_profesor integer);
       public          postgres    false                       1255    57833 1   consultar_profesores_por_grupo(character varying)    FUNCTION     �  CREATE FUNCTION public.consultar_profesores_por_grupo(nombre_grupo_ character varying) RETURNS TABLE(id_profesor integer, nombre character varying, apellido_pat character varying, apellido_mat character varying, edad integer, telefono character varying, email character varying, nombre_asignatura character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        p.id AS id_profesor,
        p.nombre,
        p.apellido_pat,
        p.apellido_mat,
        p.edad,
        p.telefono,
        p.email,
        a.nombre AS nombre_asignatura
    FROM profesores p
    INNER JOIN profesor_asignatura pa ON p.id = pa.idprofesor
    INNER JOIN asignaturas a ON pa.idasignatura = a.id
    WHERE pa.nombre_grupo = nombre_grupo_;
END;
$$;
 V   DROP FUNCTION public.consultar_profesores_por_grupo(nombre_grupo_ character varying);
       public          postgres    false            �            1255    49313    fn_alumnos_select(integer)    FUNCTION     ,  CREATE FUNCTION public.fn_alumnos_select(id_ integer) RETURNS TABLE(id integer, username character varying, nombre character varying, apellido_pat character varying, apellido_mat character varying, edad integer, grado integer)
    LANGUAGE sql
    AS $$
    SELECT * FROM alumnos WHERE id = id_;
$$;
 5   DROP FUNCTION public.fn_alumnos_select(id_ integer);
       public          postgres    false                       1255    57672    fn_consultar_profesores()    FUNCTION     �  CREATE FUNCTION public.fn_consultar_profesores() RETURNS TABLE(id_profesor integer, nombre character varying, apellido_pat character varying, apellido_mat character varying, edad integer, telefono character varying, email character varying, id_asignatura integer, nombre_asignatura character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        p.id AS id_profesor,
        p.nombre,
        p.apellido_pat,
        p.apellido_mat,
        p.edad,
        p.telefono,
        p.email,
        a.id AS id_asignatura,
        a.nombre AS nombre_asignatura
    FROM profesores p
    INNER JOIN profesor_asignatura pa ON p.id = pa.idprofesor
    INNER JOIN asignaturas a ON pa.idasignatura = a.id;
END;
$$;
 0   DROP FUNCTION public.fn_consultar_profesores();
       public          postgres    false            �            1255    57673    fn_consultartodos_profesores()    FUNCTION     �  CREATE FUNCTION public.fn_consultartodos_profesores() RETURNS TABLE(id_profesor integer, nombre character varying, apellido_pat character varying, apellido_mat character varying, edad integer, telefono character varying, email character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        p.id AS id_profesor,
        p.nombre,
        p.apellido_pat,
        p.apellido_mat,
        p.edad,
        p.telefono,
        p.email
    FROM profesores p;
END;
$$;
 5   DROP FUNCTION public.fn_consultartodos_profesores();
       public          postgres    false            �            1255    65694 1   fn_eliminargrupoconreferencias(character varying)    FUNCTION     !  CREATE FUNCTION public.fn_eliminargrupoconreferencias(p_nombre_grupo character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eliminar las referencias en la tabla profesor_asignatura
    DELETE FROM profesor_asignatura WHERE nombre_grupo = p_nombre_grupo;
    
END;
$$;
 W   DROP FUNCTION public.fn_eliminargrupoconreferencias(p_nombre_grupo character varying);
       public          postgres    false            �            1255    65695 .   fn_eliminarreferenciasgrupo(character varying)    FUNCTION       CREATE FUNCTION public.fn_eliminarreferenciasgrupo(p_nombre_grupo character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eliminar las referencias en la tabla profesor_asignatura
    DELETE FROM profesor_asignatura WHERE nombre_grupo = p_nombre_grupo;
    
END;
$$;
 T   DROP FUNCTION public.fn_eliminarreferenciasgrupo(p_nombre_grupo character varying);
       public          postgres    false            �            1255    65697 '   fn_eliminarreferenciasprofesor(integer)    FUNCTION       CREATE FUNCTION public.fn_eliminarreferenciasprofesor(p_id_profesor integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eliminar las referencias en la tabla profesor_asignatura
    DELETE FROM profesor_asignatura WHERE idprofesor = p_id_profesor;
    
END;
$$;
 L   DROP FUNCTION public.fn_eliminarreferenciasprofesor(p_id_profesor integer);
       public          postgres    false            �            1255    65696 1   fn_eliminarreferenciasprofesor(character varying)    FUNCTION       CREATE FUNCTION public.fn_eliminarreferenciasprofesor(p_id_profesor character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eliminar las referencias en la tabla profesor_asignatura
    DELETE FROM profesor_asignatura WHERE idprofesor = p_id_profesor;
    
END;
$$;
 V   DROP FUNCTION public.fn_eliminarreferenciasprofesor(p_id_profesor character varying);
       public          postgres    false            �            1255    57584 #   fn_grupos_select(character varying)    FUNCTION     �   CREATE FUNCTION public.fn_grupos_select(nombre_grupo_ character varying) RETURNS TABLE(nombre_grupo character varying, grado integer, num_estudiantes integer)
    LANGUAGE sql
    AS $$
    SELECT * FROM grupos WHERE nombre_grupo = nombre_grupo_;
$$;
 H   DROP FUNCTION public.fn_grupos_select(nombre_grupo_ character varying);
       public          postgres    false            �            1255    57556    fn_profesores_select(integer)    FUNCTION     <  CREATE FUNCTION public.fn_profesores_select(id_ integer) RETURNS TABLE(id integer, nombre character varying, apellido_pat character varying, apellido_mat character varying, edad integer, telefono character varying, email character varying)
    LANGUAGE sql
    AS $$
    SELECT * FROM profesores WHERE id = id_;
$$;
 8   DROP FUNCTION public.fn_profesores_select(id_ integer);
       public          postgres    false                       1255    49312    sp_alumnos_delete(integer) 	   PROCEDURE     �  CREATE PROCEDURE public.sp_alumnos_delete(IN id_ integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
    grupo_ varchar(50);
BEGIN
    -- Obtener el grupo al que pertenece el alumno
    SELECT grupo INTO grupo_ FROM alumnos WHERE id = id_;
    
    -- Eliminar al alumno
    DELETE FROM alumnos WHERE id = id_;

    -- Reducir el número de estudiantes en el grupo
    UPDATE grupos SET num_estudiantes = num_estudiantes - 1 WHERE nombre_grupo = grupo_;
END;
$$;
 9   DROP PROCEDURE public.sp_alumnos_delete(IN id_ integer);
       public          postgres    false            �            1255    49314 \   sp_alumnos_insert(character varying, character varying, character varying, integer, integer) 	   PROCEDURE     >  CREATE PROCEDURE public.sp_alumnos_insert(IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer)
    LANGUAGE sql
    AS $_$
	INSERT INTO alumnos (nombre, apellido_pat, apellido_mat, edad, grado)
    VALUES ($1, $2, $3, $4, $5);
$_$;
 �   DROP PROCEDURE public.sp_alumnos_insert(IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer);
       public          postgres    false                       1255    57579 o   sp_alumnos_insert(character varying, character varying, character varying, integer, integer, character varying) 	   PROCEDURE     g  CREATE PROCEDURE public.sp_alumnos_insert(IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer, IN grupo_ character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN

    
    -- Insertar el alumno en la tabla de Alumnos
    INSERT INTO alumnos (nombre, apellido_pat, apellido_mat, edad, grado, grupo)
    VALUES (nombre_, apellido_pat_, apellido_mat_, edad_, grado_, grupo_);

    -- Incrementar el número de estudiantes en el grupo
    UPDATE grupos SET num_estudiantes = num_estudiantes + 1 WHERE nombre_grupo = grupo_;

END;
$$;
 �   DROP PROCEDURE public.sp_alumnos_insert(IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer, IN grupo_ character varying);
       public          postgres    false            �            1255    49310 o   sp_alumnos_insert(character varying, character varying, character varying, character varying, integer, integer) 	   PROCEDURE     l  CREATE PROCEDURE public.sp_alumnos_insert(IN username_ character varying, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer)
    LANGUAGE sql
    AS $_$
	INSERT INTO alumnos (username, nombre, apellido_pat, apellido_mat, edad, grado)
    VALUES ($1, $2, $3, $4, $5, $6);
$_$;
 �   DROP PROCEDURE public.sp_alumnos_insert(IN username_ character varying, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer);
       public          postgres    false            �            1255    49315 e   sp_alumnos_update(integer, character varying, character varying, character varying, integer, integer) 	   PROCEDURE     \  CREATE PROCEDURE public.sp_alumnos_update(IN id integer, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer)
    LANGUAGE sql
    AS $_$
	UPDATE alumnos 
	SET nombre = $2,
		apellido_pat = $3,
		apellido_mat = $4,
		edad = $5,
		grado = $6
	WHERE id = $1;
$_$;
 �   DROP PROCEDURE public.sp_alumnos_update(IN id integer, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer);
       public          postgres    false                        1255    57595 x   sp_alumnos_update(integer, character varying, character varying, character varying, integer, integer, character varying) 	   PROCEDURE     x  CREATE PROCEDURE public.sp_alumnos_update(IN id_ integer, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer, IN nuevo_grupo character varying)
    LANGUAGE plpgsql
    AS $_$
DECLARE
    grupo_anterior varchar(50);
BEGIN
    -- Obtener el grupo anterior al que pertenecía el alumno
    SELECT alumnos.grupo INTO grupo_anterior FROM alumnos WHERE alumnos.id = id_;
    
    -- Actualizar los datos del alumno, incluyendo su nuevo grupo
    UPDATE alumnos 
    SET nombre = $2,
        apellido_pat = $3,
        apellido_mat = $4,
        edad = $5,
        grado = $6,
        grupo = $7
    WHERE alumnos.id = id_;
    
    -- Si el alumno cambió de grupo, actualizar el número de estudiantes en los grupos
    IF grupo_anterior <> nuevo_grupo THEN
        -- Restar al grupo anterior
        UPDATE grupos SET num_estudiantes = num_estudiantes - 1 WHERE grupos.nombre_grupo = grupo_anterior;
        -- Sumar al nuevo grupo
        UPDATE grupos SET num_estudiantes = num_estudiantes + 1 WHERE grupos.nombre_grupo = nuevo_grupo;
    END IF;
END;
$_$;
 �   DROP PROCEDURE public.sp_alumnos_update(IN id_ integer, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer, IN nuevo_grupo character varying);
       public          postgres    false            �            1255    49311 x   sp_alumnos_update(integer, character varying, character varying, character varying, character varying, integer, integer) 	   PROCEDURE     �  CREATE PROCEDURE public.sp_alumnos_update(IN id integer, IN username_ character varying, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer)
    LANGUAGE sql
    AS $_$
	UPDATE alumnos 
	SET username = $2,
		nombre = $3,
		apellido_pat = $4,
		apellido_mat = $5,
		edad = $6,
		grado = $7
	WHERE id = $1;
$_$;
 �   DROP PROCEDURE public.sp_alumnos_update(IN id integer, IN username_ character varying, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN grado_ integer);
       public          postgres    false            �            1255    57582 #   sp_grupos_delete(character varying) 	   PROCEDURE     �   CREATE PROCEDURE public.sp_grupos_delete(IN nombre_grupo_ character varying)
    LANGUAGE sql
    AS $$
    DELETE FROM grupos
    WHERE nombre_grupo = nombre_grupo_;
$$;
 L   DROP PROCEDURE public.sp_grupos_delete(IN nombre_grupo_ character varying);
       public          postgres    false            �            1255    57580 ,   sp_grupos_insert(character varying, integer) 	   PROCEDURE     �   CREATE PROCEDURE public.sp_grupos_insert(IN nombre_ character varying, IN grado_ integer)
    LANGUAGE sql
    AS $_$
	INSERT INTO grupos (nombre_grupo, grado)
    VALUES ($1, $2);
$_$;
 Y   DROP PROCEDURE public.sp_grupos_insert(IN nombre_ character varying, IN grado_ integer);
       public          postgres    false            �            1255    57555    sp_profesores_delete(integer) 	   PROCEDURE     �   CREATE PROCEDURE public.sp_profesores_delete(IN id_ integer)
    LANGUAGE sql
    AS $$
    DELETE FROM profesores
    WHERE id = id_;
$$;
 <   DROP PROCEDURE public.sp_profesores_delete(IN id_ integer);
       public          postgres    false            �            1255    57553 |   sp_profesores_insert(character varying, character varying, character varying, integer, character varying, character varying) 	   PROCEDURE       CREATE PROCEDURE public.sp_profesores_insert(IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN telefono_ character varying, IN email_ character varying)
    LANGUAGE sql
    AS $_$
    INSERT INTO profesores (nombre, apellido_pat, apellido_mat, edad, telefono, email)
    VALUES ($1, $2, $3, $4, $5, $6);
$_$;
 �   DROP PROCEDURE public.sp_profesores_insert(IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN telefono_ character varying, IN email_ character varying);
       public          postgres    false            �            1255    57554 �   sp_profesores_update(integer, character varying, character varying, character varying, integer, character varying, character varying) 	   PROCEDURE     �  CREATE PROCEDURE public.sp_profesores_update(IN id integer, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN telefono_ character varying, IN email_ character varying)
    LANGUAGE sql
    AS $_$
    UPDATE profesores
    SET nombre = $2,
        apellido_pat = $3,
        apellido_mat = $4,
        edad = $5,
        telefono = $6,
        email = $7
    WHERE id = $1;
$_$;
 �   DROP PROCEDURE public.sp_profesores_update(IN id integer, IN nombre_ character varying, IN apellido_pat_ character varying, IN apellido_mat_ character varying, IN edad_ integer, IN telefono_ character varying, IN email_ character varying);
       public          postgres    false            �            1259    49294    alumnos    TABLE     %  CREATE TABLE public.alumnos (
    id integer NOT NULL,
    nombre character varying(25),
    apellido_pat character varying(25),
    apellido_mat character varying(25),
    edad integer,
    grado integer,
    idgrupo integer,
    grupo character varying(50),
    puntaje integer DEFAULT 0
);
    DROP TABLE public.alumnos;
       public         heap    postgres    false            �            1259    57510    asignaturas    TABLE     h   CREATE TABLE public.asignaturas (
    id integer NOT NULL,
    nombre character varying(50) NOT NULL
);
    DROP TABLE public.asignaturas;
       public         heap    postgres    false            �            1259    57815    asignaturas_grupos    TABLE     �   CREATE TABLE public.asignaturas_grupos (
    id integer NOT NULL,
    id_asignatura integer,
    nombre_grupo character varying(50)
);
 &   DROP TABLE public.asignaturas_grupos;
       public         heap    postgres    false            �            1259    57814    asignaturas_grupos_id_seq    SEQUENCE     �   CREATE SEQUENCE public.asignaturas_grupos_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.asignaturas_grupos_id_seq;
       public          postgres    false    226            `           0    0    asignaturas_grupos_id_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.asignaturas_grupos_id_seq OWNED BY public.asignaturas_grupos.id;
          public          postgres    false    225            �            1259    57509    asignaturas_id_seq    SEQUENCE     �   CREATE SEQUENCE public.asignaturas_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.asignaturas_id_seq;
       public          postgres    false    217            a           0    0    asignaturas_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.asignaturas_id_seq OWNED BY public.asignaturas.id;
          public          postgres    false    216            �            1259    57569    grupos    TABLE     �   CREATE TABLE public.grupos (
    nombre_grupo character varying(50) NOT NULL,
    grado integer NOT NULL,
    num_estudiantes integer DEFAULT 0
);
    DROP TABLE public.grupos;
       public         heap    postgres    false            �            1259    57606    grupos_alumnos    TABLE     q   CREATE TABLE public.grupos_alumnos (
    grupo character varying(50) NOT NULL,
    id_alumno integer NOT NULL
);
 "   DROP TABLE public.grupos_alumnos;
       public         heap    postgres    false            �            1259    57538    profesor_asignatura    TABLE     �   CREATE TABLE public.profesor_asignatura (
    idprofesor integer NOT NULL,
    idasignatura integer NOT NULL,
    nombre_grupo character varying(50)
);
 '   DROP TABLE public.profesor_asignatura;
       public         heap    postgres    false            �            1259    57532 
   profesores    TABLE       CREATE TABLE public.profesores (
    id integer NOT NULL,
    nombre character varying(25),
    apellido_pat character varying(25),
    apellido_mat character varying(25),
    edad integer,
    telefono character varying(15),
    email character varying(50)
);
    DROP TABLE public.profesores;
       public         heap    postgres    false            �            1259    57659    profesores_asignaturas    TABLE     c   CREATE TABLE public.profesores_asignaturas (
    id_profesor integer,
    id_asignatura integer
);
 *   DROP TABLE public.profesores_asignaturas;
       public         heap    postgres    false            �            1259    57621    profesores_grupos    TABLE     k   CREATE TABLE public.profesores_grupos (
    id_profesor integer,
    nombre_grupo character varying(50)
);
 %   DROP TABLE public.profesores_grupos;
       public         heap    postgres    false            �            1259    57531    profesores_id_seq    SEQUENCE     �   CREATE SEQUENCE public.profesores_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.profesores_id_seq;
       public          postgres    false    219            b           0    0    profesores_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.profesores_id_seq OWNED BY public.profesores.id;
          public          postgres    false    218            �            1259    49293    usuarios_id_seq    SEQUENCE     �   CREATE SEQUENCE public.usuarios_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.usuarios_id_seq;
       public          postgres    false    215            c           0    0    usuarios_id_seq    SEQUENCE OWNED BY     B   ALTER SEQUENCE public.usuarios_id_seq OWNED BY public.alumnos.id;
          public          postgres    false    214            �           2604    49297 
   alumnos id    DEFAULT     i   ALTER TABLE ONLY public.alumnos ALTER COLUMN id SET DEFAULT nextval('public.usuarios_id_seq'::regclass);
 9   ALTER TABLE public.alumnos ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    214    215    215            �           2604    57513    asignaturas id    DEFAULT     p   ALTER TABLE ONLY public.asignaturas ALTER COLUMN id SET DEFAULT nextval('public.asignaturas_id_seq'::regclass);
 =   ALTER TABLE public.asignaturas ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    216    217            �           2604    57818    asignaturas_grupos id    DEFAULT     ~   ALTER TABLE ONLY public.asignaturas_grupos ALTER COLUMN id SET DEFAULT nextval('public.asignaturas_grupos_id_seq'::regclass);
 D   ALTER TABLE public.asignaturas_grupos ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    226    225    226            �           2604    57535    profesores id    DEFAULT     n   ALTER TABLE ONLY public.profesores ALTER COLUMN id SET DEFAULT nextval('public.profesores_id_seq'::regclass);
 <   ALTER TABLE public.profesores ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    219    218    219            N          0    49294    alumnos 
   TABLE DATA           o   COPY public.alumnos (id, nombre, apellido_pat, apellido_mat, edad, grado, idgrupo, grupo, puntaje) FROM stdin;
    public          postgres    false    215   	�       P          0    57510    asignaturas 
   TABLE DATA           1   COPY public.asignaturas (id, nombre) FROM stdin;
    public          postgres    false    217   ��       Y          0    57815    asignaturas_grupos 
   TABLE DATA           M   COPY public.asignaturas_grupos (id, id_asignatura, nombre_grupo) FROM stdin;
    public          postgres    false    226   P�       T          0    57569    grupos 
   TABLE DATA           F   COPY public.grupos (nombre_grupo, grado, num_estudiantes) FROM stdin;
    public          postgres    false    221   ��       U          0    57606    grupos_alumnos 
   TABLE DATA           :   COPY public.grupos_alumnos (grupo, id_alumno) FROM stdin;
    public          postgres    false    222   ˈ       S          0    57538    profesor_asignatura 
   TABLE DATA           U   COPY public.profesor_asignatura (idprofesor, idasignatura, nombre_grupo) FROM stdin;
    public          postgres    false    220   �       R          0    57532 
   profesores 
   TABLE DATA           c   COPY public.profesores (id, nombre, apellido_pat, apellido_mat, edad, telefono, email) FROM stdin;
    public          postgres    false    219   �       W          0    57659    profesores_asignaturas 
   TABLE DATA           L   COPY public.profesores_asignaturas (id_profesor, id_asignatura) FROM stdin;
    public          postgres    false    224   #�       V          0    57621    profesores_grupos 
   TABLE DATA           F   COPY public.profesores_grupos (id_profesor, nombre_grupo) FROM stdin;
    public          postgres    false    223   @�       d           0    0    asignaturas_grupos_id_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.asignaturas_grupos_id_seq', 9, true);
          public          postgres    false    225            e           0    0    asignaturas_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.asignaturas_id_seq', 5, true);
          public          postgres    false    216            f           0    0    profesores_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.profesores_id_seq', 13, true);
          public          postgres    false    218            g           0    0    usuarios_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.usuarios_id_seq', 33, true);
          public          postgres    false    214            �           2606    57820 *   asignaturas_grupos asignaturas_grupos_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public.asignaturas_grupos
    ADD CONSTRAINT asignaturas_grupos_pkey PRIMARY KEY (id);
 T   ALTER TABLE ONLY public.asignaturas_grupos DROP CONSTRAINT asignaturas_grupos_pkey;
       public            postgres    false    226            �           2606    57515    asignaturas asignaturas_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.asignaturas
    ADD CONSTRAINT asignaturas_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.asignaturas DROP CONSTRAINT asignaturas_pkey;
       public            postgres    false    217            �           2606    57610 "   grupos_alumnos grupos_alumnos_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public.grupos_alumnos
    ADD CONSTRAINT grupos_alumnos_pkey PRIMARY KEY (grupo, id_alumno);
 L   ALTER TABLE ONLY public.grupos_alumnos DROP CONSTRAINT grupos_alumnos_pkey;
       public            postgres    false    222    222            �           2606    57573    grupos grupos_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.grupos
    ADD CONSTRAINT grupos_pkey PRIMARY KEY (nombre_grupo);
 <   ALTER TABLE ONLY public.grupos DROP CONSTRAINT grupos_pkey;
       public            postgres    false    221            �           2606    57542 ,   profesor_asignatura profesor_asignatura_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.profesor_asignatura
    ADD CONSTRAINT profesor_asignatura_pkey PRIMARY KEY (idprofesor, idasignatura);
 V   ALTER TABLE ONLY public.profesor_asignatura DROP CONSTRAINT profesor_asignatura_pkey;
       public            postgres    false    220    220            �           2606    57537    profesores profesores_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.profesores
    ADD CONSTRAINT profesores_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.profesores DROP CONSTRAINT profesores_pkey;
       public            postgres    false    219            �           2606    49299    alumnos usuarios_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.alumnos
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id);
 ?   ALTER TABLE ONLY public.alumnos DROP CONSTRAINT usuarios_pkey;
       public            postgres    false    215            �           2606    57574    alumnos alumnos_grupo_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.alumnos
    ADD CONSTRAINT alumnos_grupo_fkey FOREIGN KEY (grupo) REFERENCES public.grupos(nombre_grupo);
 D   ALTER TABLE ONLY public.alumnos DROP CONSTRAINT alumnos_grupo_fkey;
       public          postgres    false    215    3246    221            �           2606    57821 8   asignaturas_grupos asignaturas_grupos_id_asignatura_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.asignaturas_grupos
    ADD CONSTRAINT asignaturas_grupos_id_asignatura_fkey FOREIGN KEY (id_asignatura) REFERENCES public.asignaturas(id);
 b   ALTER TABLE ONLY public.asignaturas_grupos DROP CONSTRAINT asignaturas_grupos_id_asignatura_fkey;
       public          postgres    false    3240    226    217            �           2606    57826 7   asignaturas_grupos asignaturas_grupos_nombre_grupo_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.asignaturas_grupos
    ADD CONSTRAINT asignaturas_grupos_nombre_grupo_fkey FOREIGN KEY (nombre_grupo) REFERENCES public.grupos(nombre_grupo);
 a   ALTER TABLE ONLY public.asignaturas_grupos DROP CONSTRAINT asignaturas_grupos_nombre_grupo_fkey;
       public          postgres    false    226    3246    221            �           2606    57834 #   profesor_asignatura fk_nombre_grupo    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesor_asignatura
    ADD CONSTRAINT fk_nombre_grupo FOREIGN KEY (nombre_grupo) REFERENCES public.grupos(nombre_grupo);
 M   ALTER TABLE ONLY public.profesor_asignatura DROP CONSTRAINT fk_nombre_grupo;
       public          postgres    false    221    3246    220            �           2606    57611 (   grupos_alumnos grupos_alumnos_grupo_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupos_alumnos
    ADD CONSTRAINT grupos_alumnos_grupo_fkey FOREIGN KEY (grupo) REFERENCES public.grupos(nombre_grupo);
 R   ALTER TABLE ONLY public.grupos_alumnos DROP CONSTRAINT grupos_alumnos_grupo_fkey;
       public          postgres    false    3246    222    221            �           2606    57616 ,   grupos_alumnos grupos_alumnos_id_alumno_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupos_alumnos
    ADD CONSTRAINT grupos_alumnos_id_alumno_fkey FOREIGN KEY (id_alumno) REFERENCES public.alumnos(id);
 V   ALTER TABLE ONLY public.grupos_alumnos DROP CONSTRAINT grupos_alumnos_id_alumno_fkey;
       public          postgres    false    215    3238    222            �           2606    57548 9   profesor_asignatura profesor_asignatura_idasignatura_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesor_asignatura
    ADD CONSTRAINT profesor_asignatura_idasignatura_fkey FOREIGN KEY (idasignatura) REFERENCES public.asignaturas(id);
 c   ALTER TABLE ONLY public.profesor_asignatura DROP CONSTRAINT profesor_asignatura_idasignatura_fkey;
       public          postgres    false    3240    220    217            �           2606    57543 7   profesor_asignatura profesor_asignatura_idprofesor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesor_asignatura
    ADD CONSTRAINT profesor_asignatura_idprofesor_fkey FOREIGN KEY (idprofesor) REFERENCES public.profesores(id);
 a   ALTER TABLE ONLY public.profesor_asignatura DROP CONSTRAINT profesor_asignatura_idprofesor_fkey;
       public          postgres    false    220    219    3242            �           2606    57667 @   profesores_asignaturas profesores_asignaturas_id_asignatura_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesores_asignaturas
    ADD CONSTRAINT profesores_asignaturas_id_asignatura_fkey FOREIGN KEY (id_asignatura) REFERENCES public.asignaturas(id);
 j   ALTER TABLE ONLY public.profesores_asignaturas DROP CONSTRAINT profesores_asignaturas_id_asignatura_fkey;
       public          postgres    false    217    3240    224            �           2606    57662 >   profesores_asignaturas profesores_asignaturas_id_profesor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesores_asignaturas
    ADD CONSTRAINT profesores_asignaturas_id_profesor_fkey FOREIGN KEY (id_profesor) REFERENCES public.profesores(id);
 h   ALTER TABLE ONLY public.profesores_asignaturas DROP CONSTRAINT profesores_asignaturas_id_profesor_fkey;
       public          postgres    false    3242    219    224            �           2606    57624 4   profesores_grupos profesores_grupos_id_profesor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesores_grupos
    ADD CONSTRAINT profesores_grupos_id_profesor_fkey FOREIGN KEY (id_profesor) REFERENCES public.profesores(id);
 ^   ALTER TABLE ONLY public.profesores_grupos DROP CONSTRAINT profesores_grupos_id_profesor_fkey;
       public          postgres    false    223    3242    219            �           2606    57629 5   profesores_grupos profesores_grupos_nombre_grupo_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.profesores_grupos
    ADD CONSTRAINT profesores_grupos_nombre_grupo_fkey FOREIGN KEY (nombre_grupo) REFERENCES public.grupos(nombre_grupo);
 _   ALTER TABLE ONLY public.profesores_grupos DROP CONSTRAINT profesores_grupos_nombre_grupo_fkey;
       public          postgres    false    223    221    3246            N   �   x�]�=N�@���)8ڟ�c�"�( +�i�x,�k4�)�e
W9�^��!��o����@��Ɗ�4r�DI�I#�x8�����3��5��[b�>}E���Ӹd���>SNc94;�ۼ�|�t�Op��;esT���x�B�K���wr��b�x3#n�ʾT��7��B�)�u!b���"��?������!҅(��m�,���	�}O�|��T���UJ}H'c4      P   D   x�3��M,I�=��$39��ˈӵ� ����.cN������D.����ks�*�L9��-�b���� ���      Y   6   x�ȹ  ��[�74n���@�4ժ����Eҭ)��H��r�����C�	a      T   %   x�3v�4�4�2vR\FN�F@��	�3����� O�      U      x������ � �      S   %   x�3�4�4v�`ʐ�Lp��hSN#���� )�      R   �   x�U�;N�0���)r���co�ݮ(�!!!j��k#?��P�6 ����`i,���_ߌ�J.v�#�>O��p���D�L>O�P�Rj��;%��{�-Ӈ֓u}Q�5v��c?N1���q�8~Xg�`z�(�jv����pע�:P(W+n���d����j�xgjU9蘒����Cˣ{5��<\�S�67'�R��an PJ
��7�������I�o|a#Q�ϛ�,<�����˦(�_U8s{      W      x������ � �      V      x������ � �     