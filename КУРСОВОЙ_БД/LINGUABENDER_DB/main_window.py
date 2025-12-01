import os
from PyQt5.QtWidgets import (QMainWindow, QTabWidget, QWidget, QVBoxLayout, QHBoxLayout, 
                            QTableWidget, QTableWidgetItem, QHeaderView, QLabel, QLineEdit, 
                            QPushButton, QFormLayout, QGroupBox, QDateEdit, QMessageBox,
                            QTextEdit, QComboBox, QCheckBox, QTimeEdit, QScrollArea, 
                            QSplitter, QFrame, QSizePolicy, QFileDialog)
from PyQt5.QtCore import Qt, QDate, QTime
from PyQt5.QtGui import QFont

class Procedures:
    @staticmethod
    def join_lesson(user_id, lesson_id):
        return True, f"Пользователь {user_id} присоединился к уроку {lesson_id}"

    @staticmethod
    def submit_assignment(user_id, assignment_id, submission_text, attachment_path):
        attachment = os.path.basename(attachment_path) if attachment_path else "без вложения"
        return True, f"Задание {assignment_id} от пользователя {user_id} отправлено ({attachment})"

    @staticmethod
    def post_forum_message(user_id, thread_id, post_text):
        return True, f"Сообщение в теме {thread_id} от пользователя {user_id} опубликовано"

    @staticmethod
    def update_user_info(user_id, first_name, last_name, email):
        return True, f"Данные пользователя {user_id} обновлены"

    @staticmethod
    def issue_certificate(user_id, course_id):
        return True, f"Сертификат курса {course_id} выдан пользователю {user_id}"

    @staticmethod
    def add_teacher_qualification(teacher_id, qual_type, institution, description, obtained_date, file_path):
        doc = os.path.basename(file_path) if file_path else "без документа"
        return True, f"Квалификация {qual_type} для преподавателя {teacher_id} добавлена ({doc})"

    @staticmethod
    def create_course(course_name, description, start_date, end_date, teacher_id):
        return True, f"Курс '{course_name}' создан (преподаватель {teacher_id})"

    @staticmethod
    def schedule_lesson(course_id, lesson_name, lesson_date, lesson_duration, lesson_materials):
        return True, f"Урок '{lesson_name}' для курса {course_id} запланирован на {lesson_date}"

    @staticmethod
    def grade_assignment(user_assignment_id, grade, feedback):
        return True, f"Задание {user_assignment_id} оценено на {grade}"

    @staticmethod
    def manage_user_profile(action, user_id, first_name, last_name, email, password_hash, role_id):
        return True, f"Действие {action} для пользователя {user_id} выполнено"

    @staticmethod
    def update_lesson_material(lesson_id, new_material):
        return True, f"Материалы урока {lesson_id} обновлены"

    @staticmethod
    def create_event(forum_name, course_id, created_by, description):
        return True, f"Мероприятие '{forum_name}' создано"

    @staticmethod
    def verify_teacher_document(qualification_id, verified):
        status = "подтвержден" if verified else "не подтвержден"
        return True, f"Документ {qualification_id} {status}"

    @staticmethod
    def change_user_role(user_id, new_role):
        return True, f"Роль пользователя {user_id} изменена на {new_role}"

class Views:
    @staticmethod
    def get_student_materials():
        return [
            (1, "Английский для начинающих", "Введение", "Материалы урока 1"),
            (2, "Испанский язык", "Основы", "Материалы урока 1"),
            (3, "Французский язык", "Грамматика", "Материалы урока 2")
        ]

    @staticmethod
    def get_lesson_records():
        return [
            (1, "Введение в английский", "Запись урока 1"),
            (2, "Основы испанского", "Запись урока 1"),
            (3, "Французская грамматика", "Запись урока 2")
        ]

    @staticmethod
    def get_user_activity_sum():
        return [
            (1, "Иван", "Иванов", 5),
            (2, "Петр", "Петров", 3),
            (3, "Сидор", "Сидоров", 7)
        ]

    @staticmethod
    def get_top_teachers_by_rating():
        return [
            (101, "Анна", "Смирнова", 4.8),
            (102, "Ольга", "Петрова", 4.7),
            (103, "Ирина", "Иванова", 4.5)
        ]

    @staticmethod
    def get_students_with_overdue_assignments():
        return [
            (1, "Иван", "Иванов", 1, "2023-05-01", "2023-05-03"),
            (2, "Петр", "Петров", 2, "2023-05-05", "2023-05-07")
        ]

    @staticmethod
    def get_teacher_schedule_week():
        return [
            (101, "Анна", "Смирнова", "Английский", "Введение", "2023-05-08", "10:00", "11:30"),
            (102, "Ольга", "Петрова", "Испанский", "Основы", "2023-05-09", "14:00", "15:30")
        ]

    @staticmethod
    def get_student_progress_details():
        return [
            (1, "Иван", "Иванов", "Английский", "В процессе", None),
            (2, "Петр", "Петров", "Испанский", "Завершен", "2023-04-28")
        ]

    @staticmethod
    def get_teacher_avg_rating(teacher_id):
        return 4.5

    @staticmethod
    def get_teacher_course_load(teacher_id):
        return 3

    @staticmethod
    def get_student_assignments_by_course(user_id, course_id):
        return [
            (1, "Домашнее задание 1", "2023-05-01", "2023-05-03", 5),
            (2, "Домашнее задание 2", "2023-05-08", None, None)
        ]

    @staticmethod
    def get_user_forums(user_id):
        return [
            (1, "Общий форум", "Обсуждение общих вопросов"),
            (2, "Английский язык", "Обсуждение уроков")
        ]

    @staticmethod
    def is_course_completed(user_id, course_id):
        return course_id == 2

    @staticmethod
    def get_user_activity_by_date(user_id, activity_date):
        return [
            (1, user_id, "Просмотр урока", f"{activity_date} 10:00"),
            (2, user_id, "Отправка задания", f"{activity_date} 14:30")
        ]

class MainWindow(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("LINGUABENDER - Система управления обучением")
        self.setGeometry(100, 100, 1280, 800)
        
        # Основной виджет и layout
        central_widget = QWidget()
        self.setCentralWidget(central_widget)
        
        main_layout = QHBoxLayout(central_widget)
        main_layout.setContentsMargins(5, 5, 5, 5)
        
        # Создаем разделитель для навигации и контента
        splitter = QSplitter(Qt.Horizontal)
        
        # Панель навигации
        nav_frame = QFrame()
        nav_frame.setFrameShape(QFrame.StyledPanel)
        nav_frame.setFixedWidth(200)
        nav_layout = QVBoxLayout(nav_frame)
        nav_layout.setContentsMargins(10, 20, 10, 20)
        
        # Логотип и заголовок
        logo_label = QLabel("LINGUABENDER")
        logo_label.setFont(QFont('Arial', 14, QFont.Bold))
        logo_label.setAlignment(Qt.AlignCenter)
        logo_label.setStyleSheet("color: #2c3e50; margin-bottom: 20px;")
        nav_layout.addWidget(logo_label)
        
        # Кнопки навигации
        self.student_btn = QPushButton("Студент")
        self.teacher_btn = QPushButton("Преподаватель")
        self.manager_btn = QPushButton("Менеджер")
        self.admin_btn = QPushButton("Администратор")
        self.functions_btn = QPushButton("Функции")
        
        for btn in [self.student_btn, self.teacher_btn, self.manager_btn, 
                   self.admin_btn, self.functions_btn]:
            btn.setStyleSheet("""
                QPushButton {
                    background-color: #3498db;
                    color: white;
                    border: none;
                    padding: 10px;
                    text-align: left;
                    font-size: 14px;
                    margin: 5px 0;
                    border-radius: 4px;
                }
                QPushButton:hover {
                    background-color: #2980b9;
                }
            """)
            btn.setCursor(Qt.PointingHandCursor)
            nav_layout.addWidget(btn)
        
        nav_layout.addStretch()
        
        # Контентная область
        content_frame = QFrame()
        content_frame.setFrameShape(QFrame.StyledPanel)
        content_layout = QVBoxLayout(content_frame)
        content_layout.setContentsMargins(0, 0, 0, 0)
        
        # Вкладки (скрываем стандартный виджет вкладок)
        self.tabs = QTabWidget()
        self.tabs.setTabBarAutoHide(True)
        self.tabs.tabBar().hide()
        content_layout.addWidget(self.tabs)
        
        splitter.addWidget(nav_frame)
        splitter.addWidget(content_frame)
        splitter.setSizes([200, 1080])
        
        main_layout.addWidget(splitter)
        
        # Создаем вкладки
        self.create_student_tab()
        self.create_teacher_tab()
        self.create_manager_tab()
        self.create_admin_tab()
        self.create_functions_tab()
        
        # Подключаем кнопки навигации
        self.student_btn.clicked.connect(lambda: self.tabs.setCurrentIndex(0))
        self.teacher_btn.clicked.connect(lambda: self.tabs.setCurrentIndex(1))
        self.manager_btn.clicked.connect(lambda: self.tabs.setCurrentIndex(2))
        self.admin_btn.clicked.connect(lambda: self.tabs.setCurrentIndex(3))
        self.functions_btn.clicked.connect(lambda: self.tabs.setCurrentIndex(4))
        
        # Устанавливаем стили
        self.setStyleSheet("""
            QMainWindow {
                background-color: #f5f7fa;
            }
            QGroupBox {
                border: 1px solid #ddd;
                border-radius: 5px;
                margin-top: 10px;
                padding-top: 15px;
                font-weight: bold;
            }
            QGroupBox::title {
                subcontrol-origin: margin;
                left: 10px;
                padding: 0 3px;
            }
            QTableWidget {
                border: 1px solid #ddd;
                background-color: white;
            }
            QLineEdit, QTextEdit, QDateEdit, QTimeEdit, QComboBox {
                border: 1px solid #ddd;
                border-radius: 3px;
                padding: 5px;
                background-color: white;
            }
            QPushButton {
                background-color: #3498db;
                color: white;
                border: none;
                padding: 8px 15px;
                border-radius: 4px;
            }
            QPushButton:hover {
                background-color: #2980b9;
            }
        """)
    
    def create_scroll_area(self, widget):
        """Создает область прокрутки для вкладки"""
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        scroll.setWidget(widget)
        return scroll
    
    def create_student_tab(self):
        student_tab = QWidget()
        layout = QVBoxLayout(student_tab)
        layout.setContentsMargins(20, 20, 20, 20)
        
        # Разделяем на две колонки
        columns = QHBoxLayout()
        
        # Левая колонка (Участие в обучении)
        left_column = QVBoxLayout()
        left_column.setSpacing(15)
        
        # Участие в уроках
        join_lesson_group = QGroupBox("Участие в уроках")
        join_layout = QFormLayout()
        join_layout.setLabelAlignment(Qt.AlignRight)
        
        self.join_user_id = self.create_input_field("ID пользователя:")
        self.join_lesson_id = self.create_input_field("ID урока:")
        join_btn = self.create_button("Присоединиться к уроку", self.join_lesson)
        
        join_layout.addRow("ID пользователя:", self.join_user_id)
        join_layout.addRow("ID урока:", self.join_lesson_id)
        join_layout.addRow(join_btn)
        
        join_lesson_group.setLayout(join_layout)
        left_column.addWidget(join_lesson_group)
        
        # Отправка заданий
        assignment_group = QGroupBox("Отправка задания")
        assignment_layout = QFormLayout()
        
        self.assignment_user_id = self.create_input_field("ID пользователя:")
        self.assignment_id = self.create_input_field("ID задания:")
        self.submission_text = QTextEdit()
        self.submission_text.setFixedHeight(100)
        
        # Замена поля URL на выбор файла
        self.submission_attachment = QPushButton("Выбрать файл")
        self.submission_attachment.clicked.connect(self.select_file)
        self.submission_attachment_path = QLabel("Файл не выбран")
        
        submit_btn = self.create_button("Отправить задание", self.submit_assignment)
        
        assignment_layout.addRow("ID пользователя:", self.assignment_user_id)
        assignment_layout.addRow("ID задания:", self.assignment_id)
        assignment_layout.addRow("Текст задания:", self.submission_text)
        assignment_layout.addRow("Вложение:", self.submission_attachment)
        assignment_layout.addRow(self.submission_attachment_path)
        assignment_layout.addRow(submit_btn)
        
        assignment_group.setLayout(assignment_layout)
        left_column.addWidget(assignment_group)
        
        # Правая колонка (Форум и профиль)
        right_column = QVBoxLayout()
        right_column.setSpacing(15)
        
        # Работа с форумом
        forum_group = QGroupBox("Форум")
        forum_layout = QFormLayout()
        
        self.forum_user_id = self.create_input_field("ID пользователя:")
        self.thread_id = self.create_input_field("ID темы:")
        self.post_text = QTextEdit()
        self.post_text.setFixedHeight(100)
        post_btn = self.create_button("Отправить сообщение", self.post_forum_message)
        
        forum_layout.addRow("ID пользователя:", self.forum_user_id)
        forum_layout.addRow("ID темы:", self.thread_id)
        forum_layout.addRow("Текст сообщения:", self.post_text)
        forum_layout.addRow(post_btn)
        
        forum_group.setLayout(forum_layout)
        right_column.addWidget(forum_group)
        
        # Управление профилем
        profile_group = QGroupBox("Мой профиль")
        profile_layout = QFormLayout()
        
        self.update_user_id = self.create_input_field("ID пользователя:")
        self.first_name = self.create_input_field("Имя:")
        self.last_name = self.create_input_field("Фамилия:")
        self.email = self.create_input_field("Email:")
        update_btn = self.create_button("Обновить информацию", self.update_user_info)
        
        profile_layout.addRow("ID пользователя:", self.update_user_id)
        profile_layout.addRow("Имя:", self.first_name)
        profile_layout.addRow("Фамилия:", self.last_name)
        profile_layout.addRow("Email:", self.email)
        profile_layout.addRow(update_btn)
        
        profile_group.setLayout(profile_layout)
        right_column.addWidget(profile_group)
        
        # Сертификаты
        certificate_group = QGroupBox("Мои сертификаты")
        cert_layout = QFormLayout()
        
        self.cert_user_id = self.create_input_field("ID пользователя:")
        self.cert_course_id = self.create_input_field("ID курса:")
        cert_btn = self.create_button("Получить сертификат", self.issue_certificate)
        
        cert_layout.addRow("ID пользователя:", self.cert_user_id)
        cert_layout.addRow("ID курса:", self.cert_course_id)
        cert_layout.addRow(cert_btn)
        
        certificate_group.setLayout(cert_layout)
        right_column.addWidget(certificate_group)
        
        # Добавляем представления для студентов
        student_views_group = QGroupBox("Мои материалы и активность")
        student_views_layout = QVBoxLayout()
        
        # Материалы для студентов
        materials_label = QLabel("Мои учебные материалы:")
        student_views_layout.addWidget(materials_label)
        
        materials_table = QTableWidget()
        materials_table.setColumnCount(4)
        materials_table.setHorizontalHeaderLabels(["ID студента", "Название курса", "Название урока", "Материалы"])
        materials_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        materials_data = Views.get_student_materials()
        materials_table.setRowCount(len(materials_data))
        for row, (user_id, course_name, lesson_name, lesson_materials) in enumerate(materials_data):
            materials_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            materials_table.setItem(row, 1, QTableWidgetItem(course_name))
            materials_table.setItem(row, 2, QTableWidgetItem(lesson_name))
            materials_table.setItem(row, 3, QTableWidgetItem(lesson_materials))
        
        student_views_layout.addWidget(materials_table)
        
        # Записи уроков
        records_label = QLabel("Мои записи уроков:")
        student_views_layout.addWidget(records_label)
        
        records_table = QTableWidget()
        records_table.setColumnCount(3)
        records_table.setHorizontalHeaderLabels(["ID студента", "Название урока", "Материалы"])
        records_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        records_data = Views.get_lesson_records()
        records_table.setRowCount(len(records_data))
        for row, (user_id, lesson_name, lesson_materials) in enumerate(records_data):
            records_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            records_table.setItem(row, 1, QTableWidgetItem(lesson_name))
            records_table.setItem(row, 2, QTableWidgetItem(lesson_materials))
        
        student_views_layout.addWidget(records_table)
        
        student_views_group.setLayout(student_views_layout)
        right_column.addWidget(student_views_group)
        
        # Добавляем колонки в основной layout
        columns.addLayout(left_column)
        columns.addLayout(right_column)
        layout.addLayout(columns)
        
        # Добавляем вкладку с прокруткой
        self.tabs.addTab(self.create_scroll_area(student_tab), "")
    
    def create_teacher_tab(self):
        teacher_tab = QWidget()
        layout = QVBoxLayout(teacher_tab)
        layout.setContentsMargins(20, 20, 20, 20)
        
        # Верхняя часть (Курсы и уроки)
        top_row = QHBoxLayout()
        
        # Создание курса
        course_group = QGroupBox("Создание курса")
        course_layout = QFormLayout()
        
        self.course_name = self.create_input_field("Название курса:")
        self.course_description = QTextEdit()
        self.course_description.setFixedHeight(80)
        self.start_date = QDateEdit()
        self.start_date.setDate(QDate.currentDate())
        self.end_date = QDateEdit()
        self.end_date.setDate(QDate.currentDate().addMonths(3))
        self.course_teacher_id = self.create_input_field("ID преподавателя:")
        create_course_btn = self.create_button("Создать курс", self.create_course)
        
        course_layout.addRow("Название курса:", self.course_name)
        course_layout.addRow("Описание:", self.course_description)
        course_layout.addRow("Дата начала:", self.start_date)
        course_layout.addRow("Дата окончания:", self.end_date)
        course_layout.addRow("ID преподавателя:", self.course_teacher_id)
        course_layout.addRow(create_course_btn)
        
        course_group.setLayout(course_layout)
        top_row.addWidget(course_group)
        
        # Планирование уроков
        lesson_group = QGroupBox("Планирование урока")
        lesson_layout = QFormLayout()
        
        self.lesson_course_id = self.create_input_field("ID курса:")
        self.lesson_name = self.create_input_field("Название урока:")
        self.lesson_date = QDateEdit()
        self.lesson_date.setDate(QDate.currentDate())
        self.lesson_duration = QTimeEdit()
        self.lesson_duration.setTime(QTime(1, 30))
        self.lesson_materials = QTextEdit()
        self.lesson_materials.setFixedHeight(80)
        schedule_btn = self.create_button("Запланировать урок", self.schedule_lesson)
        
        lesson_layout.addRow("ID курса:", self.lesson_course_id)
        lesson_layout.addRow("Название урока:", self.lesson_name)
        lesson_layout.addRow("Дата урока:", self.lesson_date)
        lesson_layout.addRow("Продолжительность:", self.lesson_duration)
        lesson_layout.addRow("Материалы:", self.lesson_materials)
        lesson_layout.addRow(schedule_btn)
        
        lesson_group.setLayout(lesson_layout)
        top_row.addWidget(lesson_group)
        
        layout.addLayout(top_row)
        
        # Нижняя часть (Оценки и квалификация)
        bottom_row = QHBoxLayout()
        
        # Оценка заданий
        grade_group = QGroupBox("Оценка заданий")
        grade_layout = QFormLayout()
        
        self.user_assignment_id = self.create_input_field("ID задания пользователя:")
        self.grade = self.create_input_field("Оценка:")
        self.feedback = QTextEdit()
        self.feedback.setFixedHeight(80)
        grade_btn = self.create_button("Поставить оценку", self.grade_assignment)
        
        grade_layout.addRow("ID задания пользователя:", self.user_assignment_id)
        grade_layout.addRow("Оценка:", self.grade)
        grade_layout.addRow("Комментарий:", self.feedback)
        grade_layout.addRow(grade_btn)
        
        grade_group.setLayout(grade_layout)
        bottom_row.addWidget(grade_group)
        
        # Квалификация преподавателя
        qual_group = QGroupBox("Моя квалификация")
        qual_layout = QFormLayout()
        
        self.teacher_id = self.create_input_field("ID преподавателя:")
        self.qual_type = self.create_input_field("Тип квалификации:")
        self.institution = self.create_input_field("Учреждение:")
        self.qual_description = QTextEdit()
        self.qual_description.setFixedHeight(60)
        self.obtained_date = QDateEdit()
        self.obtained_date.setDate(QDate.currentDate())
        
        # Замена поля URL на выбор файла
        self.file_btn = QPushButton("Выбрать файл")
        self.file_btn.clicked.connect(self.select_file)
        self.file_path = QLabel("Файл не выбран")
        
        qual_btn = self.create_button("Добавить квалификацию", self.add_teacher_qualification)
        
        qual_layout.addRow("ID преподавателя:", self.teacher_id)
        qual_layout.addRow("Тип квалификации:", self.qual_type)
        qual_layout.addRow("Учреждение:", self.institution)
        qual_layout.addRow("Описание:", self.qual_description)
        qual_layout.addRow("Дата получения:", self.obtained_date)
        qual_layout.addRow("Документ:", self.file_btn)
        qual_layout.addRow(self.file_path)
        qual_layout.addRow(qual_btn)
        
        qual_group.setLayout(qual_layout)
        bottom_row.addWidget(qual_group)
        
        layout.addLayout(bottom_row)
        
        # Добавляем представления для преподавателей
        teacher_views_group = QGroupBox("Мои данные и расписание")
        teacher_views_layout = QVBoxLayout()
        
        # Рейтинг преподавателя
        rating_group = QGroupBox("Мой рейтинг")
        rating_layout = QFormLayout()
        
        self.teacher_id_input = QLineEdit()
        self.teacher_id_input.setPlaceholderText("Введите ID преподавателя")
        get_rating_btn = QPushButton("Получить рейтинг")
        get_rating_btn.clicked.connect(self.show_teacher_rating)
        self.rating_result_label = QLabel()
        
        rating_layout.addRow("ID преподавателя:", self.teacher_id_input)
        rating_layout.addRow(get_rating_btn)
        rating_layout.addRow("Средний рейтинг:", self.rating_result_label)
        
        rating_group.setLayout(rating_layout)
        teacher_views_layout.addWidget(rating_group)
        
        # Расписание преподавателя
        schedule_label = QLabel("Мое расписание на неделю:")
        teacher_views_layout.addWidget(schedule_label)
        
        schedule_table = QTableWidget()
        schedule_table.setColumnCount(5)
        schedule_table.setHorizontalHeaderLabels(["Курс", "Урок", "Дата", "Начало", "Конец"])
        schedule_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        schedule_data = Views.get_teacher_schedule_week()
        schedule_table.setRowCount(len(schedule_data))
        for row, (_, _, _, course_name, lesson_name, schedule_date, start_time, end_time) in enumerate(schedule_data):
            schedule_table.setItem(row, 0, QTableWidgetItem(course_name))
            schedule_table.setItem(row, 1, QTableWidgetItem(lesson_name))
            schedule_table.setItem(row, 2, QTableWidgetItem(str(schedule_date)))
            schedule_table.setItem(row, 3, QTableWidgetItem(str(start_time)))
            schedule_table.setItem(row, 4, QTableWidgetItem(str(end_time)))
        
        teacher_views_layout.addWidget(schedule_table)
        
        teacher_views_group.setLayout(teacher_views_layout)
        layout.addWidget(teacher_views_group)
        
        self.tabs.addTab(self.create_scroll_area(teacher_tab), "")
    
    def create_manager_tab(self):
        manager_tab = QWidget()
        layout = QVBoxLayout(manager_tab)
        layout.setContentsMargins(20, 20, 20, 20)
        
        # Управление пользователями
        user_management_group = QGroupBox("Управление пользователями")
        user_management_layout = QFormLayout()
        
        self.profile_action = QComboBox()
        self.profile_action.addItems(["INSERT", "UPDATE", "DELETE"])
        self.profile_user_id = self.create_input_field("ID пользователя:")
        self.profile_first_name = self.create_input_field("Имя:")
        self.profile_last_name = self.create_input_field("Фамилия:")
        self.profile_email = self.create_input_field("Email:")
        self.profile_password = self.create_input_field("Пароль (hash):")
        self.profile_role_id = self.create_input_field("ID роли:")
        profile_btn = self.create_button("Выполнить действие", self.manage_user_profile)
        
        user_management_layout.addRow("Действие:", self.profile_action)
        user_management_layout.addRow("ID пользователя:", self.profile_user_id)
        user_management_layout.addRow("Имя:", self.profile_first_name)
        user_management_layout.addRow("Фамилия:", self.profile_last_name)
        user_management_layout.addRow("Email:", self.profile_email)
        user_management_layout.addRow("Пароль (hash):", self.profile_password)
        user_management_layout.addRow("ID роли:", self.profile_role_id)
        user_management_layout.addRow(profile_btn)
        
        user_management_group.setLayout(user_management_layout)
        layout.addWidget(user_management_group)
        
        # Управление контентом
        content_row = QHBoxLayout()
        
        # Обновление материалов
        material_group = QGroupBox("Управление материалами")
        material_layout = QFormLayout()
        
        self.material_lesson_id = self.create_input_field("ID урока:")
        self.new_material = QTextEdit()
        self.new_material.setFixedHeight(120)
        material_btn = self.create_button("Обновить материалы", self.update_lesson_material)
        
        material_layout.addRow("ID урока:", self.material_lesson_id)
        material_layout.addRow("Новые материалы:", self.new_material)
        material_layout.addRow(material_btn)
        
        material_group.setLayout(material_layout)
        content_row.addWidget(material_group)
        
        # Создание мероприятий
        event_group = QGroupBox("Организация мероприятий")
        event_layout = QFormLayout()
        
        self.event_forum_name = self.create_input_field("Название форума:")
        self.event_course_id = self.create_input_field("ID курса:")
        self.event_created_by = self.create_input_field("ID создателя:")
        self.event_description = QTextEdit()
        self.event_description.setFixedHeight(120)
        event_btn = self.create_button("Создать мероприятие", self.create_event)
        
        event_layout.addRow("Название форума:", self.event_forum_name)
        event_layout.addRow("ID курса:", self.event_course_id)
        event_layout.addRow("ID создателя:", self.event_created_by)
        event_layout.addRow("Описание:", self.event_description)
        event_layout.addRow(event_btn)
        
        event_group.setLayout(event_layout)
        content_row.addWidget(event_group)
        
        layout.addLayout(content_row)
        
        # Добавляем представления для менеджеров
        manager_views_group = QGroupBox("Аналитика и мониторинг")
        manager_views_layout = QVBoxLayout()
        
        # Активность пользователей
        activity_label = QLabel("Активность пользователей:")
        manager_views_layout.addWidget(activity_label)
        
        activity_table = QTableWidget()
        activity_table.setColumnCount(4)
        activity_table.setHorizontalHeaderLabels(["ID пользователя", "Имя", "Фамилия", "Количество активностей"])
        activity_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        activity_data = Views.get_user_activity_sum()
        activity_table.setRowCount(len(activity_data))
        for row, (user_id, first_name, last_name, activity_count) in enumerate(activity_data):
            activity_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            activity_table.setItem(row, 1, QTableWidgetItem(first_name))
            activity_table.setItem(row, 2, QTableWidgetItem(last_name))
            activity_table.setItem(row, 3, QTableWidgetItem(str(activity_count)))
        
        manager_views_layout.addWidget(activity_table)
        
        # Прогресс студентов
        progress_label = QLabel("Прогресс студентов по курсам:")
        manager_views_layout.addWidget(progress_label)
        
        progress_table = QTableWidget()
        progress_table.setColumnCount(5)
        progress_table.setHorizontalHeaderLabels(["ID студента", "Имя", "Фамилия", "Курс", "Статус"])
        progress_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        progress_data = Views.get_student_progress_details()
        progress_table.setRowCount(len(progress_data))
        for row, (user_id, first_name, last_name, course_name, status, _) in enumerate(progress_data):
            progress_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            progress_table.setItem(row, 1, QTableWidgetItem(first_name))
            progress_table.setItem(row, 2, QTableWidgetItem(last_name))
            progress_table.setItem(row, 3, QTableWidgetItem(course_name))
            progress_table.setItem(row, 4, QTableWidgetItem(status))
        
        manager_views_layout.addWidget(progress_table)
        
        manager_views_group.setLayout(manager_views_layout)
        layout.addWidget(manager_views_group)
        
        self.tabs.addTab(self.create_scroll_area(manager_tab), "")
    
    def create_admin_tab(self):
        admin_tab = QWidget()
        layout = QVBoxLayout(admin_tab)
        layout.setContentsMargins(20, 20, 20, 20)
        
        # Проверка документов
        verify_group = QGroupBox("Проверка документов преподавателей")
        verify_layout = QFormLayout()
        
        self.qualification_id = self.create_input_field("ID квалификации:")
        self.verified = QCheckBox("Подтверждено")
        verify_btn = self.create_button("Проверить документ", self.verify_teacher_document)
        
        verify_layout.addRow("ID квалификации:", self.qualification_id)
        verify_layout.addRow("Статус:", self.verified)
        verify_layout.addRow(verify_btn)
        
        verify_group.setLayout(verify_layout)
        layout.addWidget(verify_group)
        
        # Управление ролями
        role_group = QGroupBox("Управление правами доступа")
        role_layout = QFormLayout()
        
        self.role_user_id = self.create_input_field("ID пользователя:")
        self.new_role = self.create_input_field("Новая роль (ID):")
        role_btn = self.create_button("Изменить роль", self.change_user_role)
        
        role_layout.addRow("ID пользователя:", self.role_user_id)
        role_layout.addRow("Новая роль (ID):", self.new_role)
        role_layout.addRow(role_btn)
        
        role_group.setLayout(role_layout)
        layout.addWidget(role_group)
        
        # Добавляем представления для администраторов
        admin_views_group = QGroupBox("Системная информация")
        admin_views_layout = QVBoxLayout()
        
        # Топ преподавателей
        teachers_label = QLabel("Топ преподавателей по рейтингу:")
        admin_views_layout.addWidget(teachers_label)
        
        teachers_table = QTableWidget()
        teachers_table.setColumnCount(4)
        teachers_table.setHorizontalHeaderLabels(["ID преподавателя", "Имя", "Фамилия", "Средний рейтинг"])
        teachers_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        teachers_data = Views.get_top_teachers_by_rating()
        teachers_table.setRowCount(len(teachers_data))
        for row, (user_id, first_name, last_name, avg_rating) in enumerate(teachers_data):
            teachers_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            teachers_table.setItem(row, 1, QTableWidgetItem(first_name))
            teachers_table.setItem(row, 2, QTableWidgetItem(last_name))
            teachers_table.setItem(row, 3, QTableWidgetItem(f"{avg_rating:.2f}"))
        
        admin_views_layout.addWidget(teachers_table)
        
        # Просроченные задания
        overdue_label = QLabel("Студенты с просроченными заданиями:")
        admin_views_layout.addWidget(overdue_label)
        
        overdue_table = QTableWidget()
        overdue_table.setColumnCount(6)
        overdue_table.setHorizontalHeaderLabels(["ID студента", "Имя", "Фамилия", "ID задания", "Срок сдачи", "Дата сдачи"])
        overdue_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        overdue_data = Views.get_students_with_overdue_assignments()
        overdue_table.setRowCount(len(overdue_data))
        for row, (user_id, first_name, last_name, assignment_id, due_date, submission_date) in enumerate(overdue_data):
            overdue_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            overdue_table.setItem(row, 1, QTableWidgetItem(first_name))
            overdue_table.setItem(row, 2, QTableWidgetItem(last_name))
            overdue_table.setItem(row, 3, QTableWidgetItem(str(assignment_id)))
            overdue_table.setItem(row, 4, QTableWidgetItem(str(due_date)))
            overdue_table.setItem(row, 5, QTableWidgetItem(str(submission_date)))
        
        admin_views_layout.addWidget(overdue_table)
        
        admin_views_group.setLayout(admin_views_layout)
        layout.addWidget(admin_views_group)
        
        self.tabs.addTab(self.create_scroll_area(admin_tab), "")
    
    def create_functions_tab(self):
        functions_tab = QWidget()
        layout = QVBoxLayout(functions_tab)
        layout.setContentsMargins(20, 20, 20, 20)
        
        # Группа для работы с заданиями студентов
        assignments_group = QGroupBox("Задания студента по курсу")
        assignments_layout = QFormLayout()
        
        self.student_id_input = QLineEdit()
        self.student_id_input.setPlaceholderText("Введите ID студента")
        assignments_layout.addRow("ID студента:", self.student_id_input)
        
        self.course_id_input = QLineEdit()
        self.course_id_input.setPlaceholderText("Введите ID курса")
        assignments_layout.addRow("ID курса:", self.course_id_input)
        
        get_assignments_btn = QPushButton("Получить задания")
        get_assignments_btn.clicked.connect(self.show_student_assignments)
        assignments_layout.addRow(get_assignments_btn)
        
        self.assignments_table = QTableWidget()
        self.assignments_table.setColumnCount(5)
        self.assignments_table.setHorizontalHeaderLabels(["ID задания", "Описание", "Срок сдачи", "Дата сдачи", "Оценка"])
        self.assignments_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        assignments_layout.addRow(self.assignments_table)
        
        assignments_group.setLayout(assignments_layout)
        layout.addWidget(assignments_group)
        
        # Группа для форумов пользователя
        forums_group = QGroupBox("Форумы пользователя")
        forums_layout = QFormLayout()
        
        self.forum_user_id_input = QLineEdit()
        self.forum_user_id_input.setPlaceholderText("Введите ID пользователя")
        forums_layout.addRow("ID пользователя:", self.forum_user_id_input)
        
        get_forums_btn = QPushButton("Получить форумы")
        get_forums_btn.clicked.connect(self.show_user_forums)
        forums_layout.addRow(get_forums_btn)
        
        self.forums_table = QTableWidget()
        self.forums_table.setColumnCount(3)
        self.forums_table.setHorizontalHeaderLabels(["ID форума", "Название", "Описание"])
        self.forums_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        forums_layout.addRow(self.forums_table)
        
        forums_group.setLayout(forums_layout)
        layout.addWidget(forums_group)
        
        # Группа для проверки завершения курса
        course_complete_group = QGroupBox("Проверка завершения курса")
        complete_layout = QFormLayout()
        
        self.complete_user_id_input = QLineEdit()
        self.complete_user_id_input.setPlaceholderText("Введите ID пользователя")
        complete_layout.addRow("ID пользователя:", self.complete_user_id_input)
        
        self.complete_course_id_input = QLineEdit()
        self.complete_course_id_input.setPlaceholderText("Введите ID курса")
        complete_layout.addRow("ID курса:", self.complete_course_id_input)
        
        check_complete_btn = QPushButton("Проверить")
        check_complete_btn.clicked.connect(self.check_course_completion)
        complete_layout.addRow(check_complete_btn)
        
        self.complete_result_label = QLabel()
        complete_layout.addRow("Результат:", self.complete_result_label)
        
        course_complete_group.setLayout(complete_layout)
        layout.addWidget(course_complete_group)
        
        # Группа для активности пользователя по дате
        activity_group = QGroupBox("Активность пользователя по дате")
        activity_layout = QFormLayout()
        
        self.activity_user_id_input = QLineEdit()
        self.activity_user_id_input.setPlaceholderText("Введите ID пользователя")
        activity_layout.addRow("ID пользователя:", self.activity_user_id_input)
        
        self.activity_date_input = QDateEdit()
        self.activity_date_input.setDate(QDate.currentDate())
        self.activity_date_input.setCalendarPopup(True)
        activity_layout.addRow("Дата:", self.activity_date_input)
        
        get_activity_btn = QPushButton("Получить активность")
        get_activity_btn.clicked.connect(self.show_user_activity)
        activity_layout.addRow(get_activity_btn)
        
        self.activity_table = QTableWidget()
        self.activity_table.setColumnCount(3)
        self.activity_table.setHorizontalHeaderLabels(["ID активности", "Описание", "Дата и время"])
        self.activity_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        activity_layout.addRow(self.activity_table)
        
        activity_group.setLayout(activity_layout)
        layout.addWidget(activity_group)
        
        self.tabs.addTab(self.create_scroll_area(functions_tab), "")
    
    # Вспомогательные методы
    def create_input_field(self, placeholder=""):
        field = QLineEdit()
        field.setPlaceholderText(placeholder)
        field.setStyleSheet("padding: 6px;")
        return field
    
    def create_button(self, text, handler):
        btn = QPushButton(text)
        btn.clicked.connect(handler)
        btn.setCursor(Qt.PointingHandCursor)
        btn.setStyleSheet("""
            QPushButton {
                background-color: #2ecc71;
                color: white;
                border: none;
                padding: 8px 15px;
                border-radius: 4px;
                font-weight: bold;
            }
            QPushButton:hover {
                background-color: #27ae60;
            }
        """)
        return btn
    
    def select_file(self):
        """Метод для выбора файла"""
        file_name, _ = QFileDialog.getOpenFileName(self, "Выберите файл", "", "Все файлы (*)")
        if file_name:
            # Определяем, какое поле обновлять
            if self.sender() == self.submission_attachment:
                self.submission_attachment_path.setText(os.path.basename(file_name))
                self.selected_submission_file = file_name
            elif self.sender() == self.file_btn:
                self.file_path.setText(os.path.basename(file_name))
                self.selected_qualification_file = file_name
    
    # Методы для обработки действий студентов
    def join_lesson(self):
        try:
            user_id = int(self.join_user_id.text())
            lesson_id = int(self.join_lesson_id.text())
            success, message = Procedures.join_lesson(user_id, lesson_id)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и урока")

    def submit_assignment(self):
        try:
            user_id = int(self.assignment_user_id.text())
            assignment_id = int(self.assignment_id.text())
            submission_text = self.submission_text.toPlainText()
            
            # Получаем путь к выбранному файлу
            attachment_path = getattr(self, 'selected_submission_file', None)
            
            success, message = Procedures.submit_assignment(
                user_id, assignment_id, submission_text, attachment_path
            )
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и задания")

    def post_forum_message(self):
        try:
            user_id = int(self.forum_user_id.text())
            thread_id = int(self.thread_id.text())
            post_text = self.post_text.toPlainText()
            
            success, message = Procedures.post_forum_message(user_id, thread_id, post_text)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и темы")

    def update_user_info(self):
        try:
            user_id = int(self.update_user_id.text())
            first_name = self.first_name.text()
            last_name = self.last_name.text()
            email = self.email.text()
            
            success, message = Procedures.update_user_info(user_id, first_name, last_name, email)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    def issue_certificate(self):
        try:
            user_id = int(self.cert_user_id.text())
            course_id = int(self.cert_course_id.text())
            
            success, message = Procedures.issue_certificate(user_id, course_id)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и курса")

    # Методы для обработки действий преподавателей
    def add_teacher_qualification(self):
        try:
            teacher_id = int(self.teacher_id.text())
            qual_type = self.qual_type.text()
            institution = self.institution.text()
            description = self.qual_description.toPlainText()
            obtained_date = self.obtained_date.date().toString("yyyy-MM-dd")
            
            # Получаем путь к выбранному файлу
            file_path = getattr(self, 'selected_qualification_file', None)
            
            success, message = Procedures.add_teacher_qualification(
                teacher_id, qual_type, institution, description, obtained_date, file_path
            )
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    def create_course(self):
        try:
            course_name = self.course_name.text()
            description = self.course_description.toPlainText()
            start_date = self.start_date.date().toString("yyyy-MM-dd")
            end_date = self.end_date.date().toString("yyyy-MM-dd")
            teacher_id = int(self.course_teacher_id.text())
            
            success, message = Procedures.create_course(
                course_name, description, start_date, end_date, teacher_id
            )
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    def schedule_lesson(self):
        try:
            course_id = int(self.lesson_course_id.text())
            lesson_name = self.lesson_name.text()
            lesson_date = self.lesson_date.date().toString("yyyy-MM-dd")
            lesson_duration = self.lesson_duration.time().toString("HH:mm:ss")
            lesson_materials = self.lesson_materials.toPlainText()
            
            success, message = Procedures.schedule_lesson(
                course_id, lesson_name, lesson_date, lesson_duration, lesson_materials
            )
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    def grade_assignment(self):
        try:
            user_assignment_id = int(self.user_assignment_id.text())
            grade = int(self.grade.text())
            feedback = self.feedback.toPlainText()
            
            success, message = Procedures.grade_assignment(user_assignment_id, grade, feedback)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    # Методы для обработки действий менеджеров
    def manage_user_profile(self):
        try:
            action = self.profile_action.currentText()
            user_id = int(self.profile_user_id.text()) if self.profile_user_id.text() else None
            first_name = self.profile_first_name.text() or None
            last_name = self.profile_last_name.text() or None
            email = self.profile_email.text() or None
            password_hash = self.profile_password.text() or None
            role_id = int(self.profile_role_id.text()) if self.profile_role_id.text() else None
            
            success, message = Procedures.manage_user_profile(
                action, user_id, first_name, last_name, email, password_hash, role_id
            )
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    def update_lesson_material(self):
        try:
            lesson_id = int(self.material_lesson_id.text())
            new_material = self.new_material.toPlainText()
            
            success, message = Procedures.update_lesson_material(lesson_id, new_material)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректный ID урока")

    def create_event(self):
        try:
            forum_name = self.event_forum_name.text()
            course_id = int(self.event_course_id.text())
            created_by = int(self.event_created_by.text())
            description = self.event_description.toPlainText()
            
            success, message = Procedures.create_event(forum_name, course_id, created_by, description)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные данные")

    # Методы для обработки действий администраторов
    def verify_teacher_document(self):
        try:
            qualification_id = int(self.qualification_id.text())
            verified = self.verified.isChecked()
            
            success, message = Procedures.verify_teacher_document(qualification_id, verified)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректный ID квалификации")

    def change_user_role(self):
        try:
            user_id = int(self.role_user_id.text())
            new_role = int(self.new_role.text())
            
            success, message = Procedures.change_user_role(user_id, new_role)
            self.show_message(success, message)
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и роли")

    # Методы для работы с представлениями
    def show_teacher_rating(self):
        try:
            teacher_id = int(self.teacher_id_input.text())
            rating = Views.get_teacher_avg_rating(teacher_id)
            self.rating_result_label.setText(f"{rating:.2f}")
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректный ID преподавателя")
    
    def show_student_assignments(self):
        try:
            user_id = int(self.student_id_input.text())
            course_id = int(self.course_id_input.text())
            assignments = Views.get_student_assignments_by_course(user_id, course_id)
            
            self.assignments_table.setRowCount(len(assignments))
            for row, (assignment_id, description, due_date, submission_date, grade) in enumerate(assignments):
                self.assignments_table.setItem(row, 0, QTableWidgetItem(str(assignment_id)))
                self.assignments_table.setItem(row, 1, QTableWidgetItem(description))
                self.assignments_table.setItem(row, 2, QTableWidgetItem(str(due_date)))
                self.assignments_table.setItem(row, 3, QTableWidgetItem(str(submission_date) if submission_date else "-"))
                self.assignments_table.setItem(row, 4, QTableWidgetItem(str(grade) if grade else "-"))
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID студента и курса")
    
    def show_user_forums(self):
        try:
            user_id = int(self.forum_user_id_input.text())
            forums = Views.get_user_forums(user_id)
            
            self.forums_table.setRowCount(len(forums))
            for row, (forum_id, forum_name, description) in enumerate(forums):
                self.forums_table.setItem(row, 0, QTableWidgetItem(str(forum_id)))
                self.forums_table.setItem(row, 1, QTableWidgetItem(forum_name))
                self.forums_table.setItem(row, 2, QTableWidgetItem(description))
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректный ID пользователя")
    
    def check_course_completion(self):
        try:
            user_id = int(self.complete_user_id_input.text())
            course_id = int(self.complete_course_id_input.text())
            completed = Views.is_course_completed(user_id, course_id)
            self.complete_result_label.setText("Курс завершен" if completed else "Курс не завершен")
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и курса")
    
    def show_user_activity(self):
        try:
            user_id = int(self.activity_user_id_input.text())
            activity_date = self.activity_date_input.date().toString("yyyy-MM-dd")
            activities = Views.get_user_activity_by_date(user_id, activity_date)
            
            self.activity_table.setRowCount(len(activities))
            for row, (activity_id, _, description, activity_datetime) in enumerate(activities):
                self.activity_table.setItem(row, 0, QTableWidgetItem(str(activity_id)))
                self.activity_table.setItem(row, 1, QTableWidgetItem(description))
                self.activity_table.setItem(row, 2, QTableWidgetItem(str(activity_datetime)))
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректный ID пользователя")

    # Вспомогательный метод для показа сообщений
    def show_message(self, success, message):
        if success:
            QMessageBox.information(self, "Успех", message)
        else:
            QMessageBox.warning(self, "Ошибка", message)


if __name__ == "__main__":
    import sys
    from PyQt5.QtWidgets import QApplication
    
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())