INSERT INTO users(email, password, full_name, role, status)
VALUES('admin@gmail.com', '1234qwer', 'Quan tri vien', 'admin', 'active');

INSERT INTO categories (category_name, description, status)
VALUES (
        N'Halloween Collection',
        N'Halloween Collection',
        'active'
    ),
    (N'Pokemon go collection', N'Pokemon go collection', 'active'),
    (
        N'Pokemon Trainners',
        N'Pokemon Trainners',
        'active'
    ),
    (N'Spring Collection', N'Spring Collection', 'active');