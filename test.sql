CREATE OR REPLACE PROCEDURE 
GSSPR.prgsspr_generate_blank(
    p_number_s IN NUMBER,
    p_number_po IN NUMBER,
    p_series IN VARCHAR2,
    p_cre_user IN VARCHAR2
) IS
BEGIN
    -- Вставляем только уникальные записи
    INSERT INTO TGSSPR_SPECIAL_PRODUCTS (id, blank_number, cre_user, cre_date, BLANK_STATUS, SERIES)
    SELECT 
        SEQ_SPEC_PROD_ID.NEXTVAL, 
        n AS blank_number, 
        p_cre_user, 
        SYSDATE, 
        1 AS BLANK_STATUS, 
        p_series
    FROM (
        WITH Numbers(n) AS (
            SELECT p_number_s AS n FROM dual
            UNION ALL
            SELECT n + 1 FROM Numbers WHERE n < p_number_po
        )
        SELECT n FROM Numbers
    )
    WHERE NOT EXISTS (
        SELECT 1 
        FROM TGSSPR_SPECIAL_PRODUCTS sp 
        WHERE sp.blank_number = n 
          AND sp.series = p_series
    );

    COMMIT;

    -- Вставляем в fiscal только уникальные записи
    INSERT INTO TGSSPR_SPECIAL_PRODUCTS_fiscal
    SELECT * 
    FROM TGSSPR_SPECIAL_PRODUCTS s 
    WHERE NOT EXISTS (
        SELECT 1 
        FROM TGSSPR_SPECIAL_PRODUCTS_fiscal f 
        WHERE s.id = f.id
    );
END;
/
