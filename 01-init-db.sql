

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

DROP TABLE IF EXISTS public.user;

CREATE TABLE IF NOT EXISTS public."user"
(
    id bigint primary key generated always as identity,
    email text COLLATE pg_catalog."default" NOT NULL,
    password_hash text COLLATE pg_catalog."default" NOT NULL,    
    CONSTRAINT email_unique UNIQUE (email)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


DROP TABLE IF EXISTS public.product;

CREATE TABLE IF NOT EXISTS public.product
(
    product_id uuid DEFAULT uuid_generate_v4() NOT NULL,    
    name text COLLATE pg_catalog."default" NOT NULL,
    status smallint CHECK (status IN ('1', '0') NOT NULL,
	stock bigint NOT NULL,
    description text COLLATE pg_catalog."default",
    price NUMERIC(7,2),
    currency TEXT CHECK (currency IN ('USD', 'EUR', 'GBP'))
    created_by bigint NOT NULL,
    created_at timestamp without time zone NOT NULL,
    modified_by bigint NOT NULL,
    modified_at timestamp without time zone NOT NULL,
    CONSTRAINT product_pkey PRIMARY KEY (product_id),            
)
    