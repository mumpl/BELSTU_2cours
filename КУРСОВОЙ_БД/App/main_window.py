from views import Views
from database import Procedures
import datetime
from PyQt5.QtWidgets import (QMainWindow, QTabWidget, QWidget, QVBoxLayout, QTableWidget, 
                             QTableWidgetItem, QHeaderView, QLabel, QLineEdit, QPushButton, 
                             QFormLayout, QGroupBox, QHBoxLayout, QDateEdit, QMessageBox,
                             QTextEdit, QComboBox, QCheckBox, QTimeEdit, QScrollArea, QFrame)
from PyQt5.QtCore import Qt, QDate, QTime
from PyQt5.QtGui import QFont
import datetime
from PyQt5.QtWidgets import QFileDialog

class MainWindow(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("LINGUABENDER DB Application")
        self.setGeometry(100, 100, 1200, 800)

        # Установка стиля для всего приложения
        self.setStyleSheet("""
            QMainWindow {
                background-color: #f5f5f5;
            }
            QTabWidget::pane {
                border: 1px solid #ccc;
                background: white;
            }
            QTabBar::tab {
                background: #e0e0e0;
                border: 1px solid #ccc;
                padding: 8px;
                border-top-left-radius: 4px;
                border-top-right-radius: 4px;
            }
            QTabBar::tab:selected {
                background: white;
                border-bottom-color: white;
            }
            QGroupBox {
                border: 1px solid #ddd;
                border-radius: 5px;
                margin-top: 10px;
                padding-top: 15px;
            }
            QGroupBox::title {
                subcontrol-origin: margin;
                left: 10px;
                padding: 0 3px;
            }
            QPushButton {
                background-color: #4CAF50;
                color: white;
                border: none;
                padding: 8px 16px;
                text-align: center;
                text-decoration: none;
                font-size: 14px;
                margin: 4px 2px;
                border-radius: 4px;
            }
            QPushButton:hover {
                background-color: #45a049;
            }
            QTableWidget {
                background-color: white;
                alternate-background-color: #f9f9f9;
                gridline-color: #ddd;
            }
            QHeaderView::section {
                background-color: #f1f1f1;
                padding: 5px;
                border: none;
            }
            QLineEdit, QTextEdit, QDateEdit, QTimeEdit, QComboBox {
                border: 1px solid #ddd;
                padding: 5px;
                border-radius: 3px;
            }
        """)

        
        
        self.tabs = QTabWidget()
        self.setCentralWidget(self.tabs)

        self.create_student_tab()
        self.create_teacher_tab()
        self.create_manager_tab()
        self.create_admin_tab()
        self.create_functions_tab()

        # Добавляем новые методы для работы с файлами
    def select_attachment_file(self):
        """Выбор файла для вложения к заданию"""
        file_path, _ = QFileDialog.getOpenFileName(
            self, 
            "Выберите файл для задания", 
            "", 
            "Все файлы (*)"
        )
        if file_path:
            self.submission_attachment.setText(file_path)

    def select_qualification_file(self):
        """Выбор файла для квалификации преподавателя"""
        file_path, _ = QFileDialog.getOpenFileName(
            self, 
            "Выберите файл квалификации", 
            "", 
            "Все файлы (*)"
        )
        if file_path:
            self.file_url.setText(file_path)

    def create_scrollable_tab(self, layout):
        """Создает прокручиваемую вкладку с заданным layout"""
        widget = QWidget()
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        scroll.setWidget(widget)
        widget.setLayout(layout)
        return scroll
        
    def create_student_tab(self):
        """Вкладка для студента с прокруткой"""
        tab = QWidget()
        layout = QVBoxLayout(tab)
        
        # Создаем главный контейнер с вертикальным layout
        main_container = QVBoxLayout()
        
        # Разделы для студента
        self.create_student_materials_section(main_container)
        self.create_student_lesson_records_section(main_container)
        self.create_student_actions_section(main_container)
        
        # Устанавливаем главный контейнер в прокручиваемую область
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        content = QWidget()
        content.setLayout(main_container)
        scroll.setWidget(content)
        
        layout.addWidget(scroll)
        self.tabs.addTab(tab, "Студент")
        
    def create_student_materials_section(self, layout):
        """Раздел с учебными материалами для студентов"""
        group = QGroupBox("Учебные материалы")
        group.setFont(QFont("Arial", 10, QFont.Bold))
        group_layout = QVBoxLayout()
        
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
        
        group_layout.addWidget(materials_table)
        group.setLayout(group_layout)
        layout.addWidget(group)
        
    def create_student_lesson_records_section(self, layout):
        """Раздел с записями уроков для студентов"""
        group = QGroupBox("Записи уроков")
        group.setFont(QFont("Arial", 10, QFont.Bold))
        group_layout = QVBoxLayout()
        
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
        
        group_layout.addWidget(records_table)
        group.setLayout(group_layout)
        layout.addWidget(group)
        
    def create_student_actions_section(self, layout):
        """Раздел с действиями для студента"""
        actions_container = QHBoxLayout()
        
        # Левая колонка с формами
        left_column = QVBoxLayout()
        
        # Участие в уроках
        join_group = QGroupBox("Участие в уроках")
        join_group.setFont(QFont("Arial", 10, QFont.Bold))
        join_layout = QFormLayout()
        self.join_user_id = QLineEdit()
        self.join_lesson_id = QLineEdit()
        join_btn = QPushButton("Присоединиться к уроку")
        join_btn.clicked.connect(self.join_lesson)
        join_layout.addRow("ID пользователя:", self.join_user_id)
        join_layout.addRow("ID урока:", self.join_lesson_id)
        join_layout.addRow(join_btn)
        join_group.setLayout(join_layout)
        left_column.addWidget(join_group)
        
        # Отправка заданий
        assignment_group = QGroupBox("Отправка задания")
        assignment_group.setFont(QFont("Arial", 10, QFont.Bold))
        assignment_layout = QFormLayout()
        self.assignment_user_id = QLineEdit()
        self.assignment_id = QLineEdit()
        self.submission_text = QTextEdit()
    
        # Заменяем QLineEdit для вложения на кнопку выбора файла
        self.submission_attachment = QLineEdit()
        self.submission_attachment.setReadOnly(True)  # Сделаем поле только для чтения
        attachment_btn = QPushButton("Выбрать файл")
        attachment_btn.clicked.connect(self.select_attachment_file)
    
        attachment_layout = QHBoxLayout()
        attachment_layout.addWidget(self.submission_attachment)
        attachment_layout.addWidget(attachment_btn)
    
        submit_btn = QPushButton("Отправить задание")
        submit_btn.clicked.connect(self.submit_assignment)
    
        assignment_layout.addRow("ID пользователя:", self.assignment_user_id)
        assignment_layout.addRow("ID задания:", self.assignment_id)
        assignment_layout.addRow("Текст задания:", self.submission_text)
        assignment_layout.addRow("Вложение:", attachment_layout)  # Измененная строка
        assignment_layout.addRow(submit_btn)
        assignment_group.setLayout(assignment_layout)
        left_column.addWidget(assignment_group)
        
        # Правая колонка с формами
        right_column = QVBoxLayout()
        
        # Форум
        forum_group = QGroupBox("Форум")
        forum_group.setFont(QFont("Arial", 10, QFont.Bold))
        forum_layout = QFormLayout()
        self.forum_user_id = QLineEdit()
        self.thread_id = QLineEdit()
        self.post_text = QTextEdit()
        post_btn = QPushButton("Отправить сообщение")
        post_btn.clicked.connect(self.post_forum_message)
        forum_layout.addRow("ID пользователя:", self.forum_user_id)
        forum_layout.addRow("ID темы:", self.thread_id)
        forum_layout.addRow("Текст сообщения:", self.post_text)
        forum_layout.addRow(post_btn)
        forum_group.setLayout(forum_layout)
        right_column.addWidget(forum_group)
        
        # Обновление информации
        update_group = QGroupBox("Обновление информации")
        update_group.setFont(QFont("Arial", 10, QFont.Bold))
        update_layout = QFormLayout()
        self.update_user_id = QLineEdit()
        self.first_name = QLineEdit()
        self.last_name = QLineEdit()
        self.email = QLineEdit()
        update_btn = QPushButton("Обновить информацию")
        update_btn.clicked.connect(self.update_user_info)
        update_layout.addRow("ID пользователя:", self.update_user_id)
        update_layout.addRow("Имя:", self.first_name)
        update_layout.addRow("Фамилия:", self.last_name)
        update_layout.addRow("Email:", self.email)
        update_layout.addRow(update_btn)
        update_group.setLayout(update_layout)
        right_column.addWidget(update_group)
        
        # Сертификат
        cert_group = QGroupBox("Сертификат")
        cert_group.setFont(QFont("Arial", 10, QFont.Bold))
        cert_layout = QFormLayout()
        self.cert_user_id = QLineEdit()
        self.cert_course_id = QLineEdit()
        cert_btn = QPushButton("Получить сертификат")
        cert_btn.clicked.connect(self.issue_certificate)
        cert_layout.addRow("ID пользователя:", self.cert_user_id)
        cert_layout.addRow("ID курса:", self.cert_course_id)
        cert_layout.addRow(cert_btn)
        cert_group.setLayout(cert_layout)
        right_column.addWidget(cert_group)
        
        actions_container.addLayout(left_column)
        actions_container.addLayout(right_column)
        layout.addLayout(actions_container)

    def create_teacher_tab(self):
        """Вкладка для преподавателя"""
        tab = QWidget()
        layout = QVBoxLayout(tab)
        
        # Создаем главный контейнер с вертикальным layout
        main_container = QVBoxLayout()
        
        # Разделы для преподавателя
        self.create_teacher_info_section(main_container)
        self.create_teacher_actions_section(main_container)
        
        # Устанавливаем главный контейнер в прокручиваемую область
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        content = QWidget()
        content.setLayout(main_container)
        scroll.setWidget(content)
        
        layout.addWidget(scroll)
        self.tabs.addTab(tab, "Преподаватель")
        
    def create_teacher_info_section(self, layout):
        """Раздел с информацией для преподавателя"""
        info_container = QHBoxLayout()
        
        # Рейтинг преподавателя
        rating_group = QGroupBox("Рейтинг преподавателя")
        rating_group.setFont(QFont("Arial", 10, QFont.Bold))
        rating_layout = QFormLayout()
        self.teacher_id_input = QLineEdit()
        get_rating_btn = QPushButton("Получить рейтинг")
        get_rating_btn.clicked.connect(self.show_teacher_rating)
        self.rating_result_label = QLabel("—")
        rating_layout.addRow("ID преподавателя:", self.teacher_id_input)
        rating_layout.addRow(get_rating_btn)
        rating_layout.addRow("Средний рейтинг:", self.rating_result_label)
        rating_group.setLayout(rating_layout)
        info_container.addWidget(rating_group)
        
        # Загруженность преподавателя
        load_group = QGroupBox("Загруженность преподавателя")
        load_group.setFont(QFont("Arial", 10, QFont.Bold))
        load_layout = QFormLayout()
        self.teacher_load_id_input = QLineEdit()
        get_load_btn = QPushButton("Получить загруженность")
        get_load_btn.clicked.connect(self.show_teacher_load)
        self.load_result_label = QLabel("—")
        load_layout.addRow("ID преподавателя:", self.teacher_load_id_input)
        load_layout.addRow(get_load_btn)
        load_layout.addRow("Активных курсов:", self.load_result_label)
        load_group.setLayout(load_layout)
        info_container.addWidget(load_group)
        
        layout.addLayout(info_container)
        
    def create_teacher_actions_section(self, layout):
        """Раздел с действиями для преподавателя"""
        actions_container = QHBoxLayout()
        
        # Левая колонка с формами
        left_column = QVBoxLayout()
        
        # Квалификация преподавателя
        qual_group = QGroupBox("Квалификация преподавателя")
        qual_group.setFont(QFont("Arial", 10, QFont.Bold))
        qual_layout = QFormLayout()
        self.teacher_id = QLineEdit()
        self.qual_type = QLineEdit()
        self.institution = QLineEdit()
        self.qual_description = QTextEdit()
        self.obtained_date = QDateEdit()
        self.obtained_date.setDate(QDate.currentDate())
    
        # Заменяем QLineEdit для файла на кнопку выбора файла
        self.file_url = QLineEdit()
        self.file_url.setReadOnly(True)
        file_btn = QPushButton("Выбрать файл")
        file_btn.clicked.connect(self.select_qualification_file)
    
        file_layout = QHBoxLayout()
        file_layout.addWidget(self.file_url)
        file_layout.addWidget(file_btn)
    
        qual_btn = QPushButton("Добавить квалификацию")
        qual_btn.clicked.connect(self.add_teacher_qualification)
    
        qual_layout.addRow("ID преподавателя:", self.teacher_id)
        qual_layout.addRow("Тип квалификации:", self.qual_type)
        qual_layout.addRow("Учреждение:", self.institution)
        qual_layout.addRow("Описание:", self.qual_description)
        qual_layout.addRow("Дата получения:", self.obtained_date)
        qual_layout.addRow("Файл документа:", file_layout)  # Измененная строка
        qual_layout.addRow(qual_btn)
        qual_group.setLayout(qual_layout)
        left_column.addWidget(qual_group)
        
        # Правая колонка с формами
        right_column = QVBoxLayout()
        
        # Создание курса
        course_group = QGroupBox("Создание курса")
        course_group.setFont(QFont("Arial", 10, QFont.Bold))
        course_layout = QFormLayout()
        self.course_name = QLineEdit()
        self.course_description = QTextEdit()
        self.start_date = QDateEdit()
        self.start_date.setDate(QDate.currentDate())
        self.end_date = QDateEdit()
        self.end_date.setDate(QDate.currentDate().addMonths(3))
        self.course_teacher_id = QLineEdit()
        create_course_btn = QPushButton("Создать курс")
        create_course_btn.clicked.connect(self.create_course)
        course_layout.addRow("Название курса:", self.course_name)
        course_layout.addRow("Описание:", self.course_description)
        course_layout.addRow("Дата начала:", self.start_date)
        course_layout.addRow("Дата окончания:", self.end_date)
        course_layout.addRow("ID преподавателя:", self.course_teacher_id)
        course_layout.addRow(create_course_btn)
        course_group.setLayout(course_layout)
        right_column.addWidget(course_group)
        
        # Планирование урока
        lesson_group = QGroupBox("Планирование урока")
        lesson_group.setFont(QFont("Arial", 10, QFont.Bold))
        lesson_layout = QFormLayout()
        self.lesson_course_id = QLineEdit()
        self.lesson_name = QLineEdit()
        self.lesson_date = QDateEdit()
        self.lesson_date.setDate(QDate.currentDate())
        self.lesson_duration = QTimeEdit()
        self.lesson_duration.setTime(QTime(1, 30))
        self.lesson_materials = QTextEdit()
        schedule_btn = QPushButton("Запланировать урок")
        schedule_btn.clicked.connect(self.schedule_lesson)
        lesson_layout.addRow("ID курса:", self.lesson_course_id)
        lesson_layout.addRow("Название урока:", self.lesson_name)
        lesson_layout.addRow("Дата урока:", self.lesson_date)
        lesson_layout.addRow("Продолжительность:", self.lesson_duration)
        lesson_layout.addRow("Материалы:", self.lesson_materials)
        lesson_layout.addRow(schedule_btn)
        lesson_group.setLayout(lesson_layout)
        right_column.addWidget(lesson_group)
        
        # Оценка заданий
        grade_group = QGroupBox("Оценка заданий")
        grade_group.setFont(QFont("Arial", 10, QFont.Bold))
        grade_layout = QFormLayout()
        self.user_assignment_id = QLineEdit()
        self.grade = QLineEdit()
        self.feedback = QTextEdit()
        grade_btn = QPushButton("Поставить оценку")
        grade_btn.clicked.connect(self.grade_assignment)
        grade_layout.addRow("ID задания пользователя:", self.user_assignment_id)
        grade_layout.addRow("Оценка:", self.grade)
        grade_layout.addRow("Комментарий:", self.feedback)
        grade_layout.addRow(grade_btn)
        grade_group.setLayout(grade_layout)
        right_column.addWidget(grade_group)
        
        actions_container.addLayout(left_column)
        actions_container.addLayout(right_column)
        layout.addLayout(actions_container)
        
    def create_manager_tab(self):
        """Вкладка для менеджера"""
        tab = QWidget()
        layout = QVBoxLayout(tab)
        
        # Создаем главный контейнер с вертикальным layout
        main_container = QVBoxLayout()
        
        # Разделы для менеджера
        self.create_manager_tables_section(main_container)
        self.create_manager_actions_section(main_container)
        
        # Устанавливаем главный контейнер в прокручиваемую область
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        content = QWidget()
        content.setLayout(main_container)
        scroll.setWidget(content)
        
        layout.addWidget(scroll)
        self.tabs.addTab(tab, "Менеджер")
        
    def create_manager_tables_section(self, layout):
        """Раздел с таблицами для менеджера"""
        # Активность пользователей
        activity_group = QGroupBox("Активность пользователей")
        activity_group.setFont(QFont("Arial", 10, QFont.Bold))
        activity_layout = QVBoxLayout()
        
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
        
        activity_layout.addWidget(activity_table)
        activity_group.setLayout(activity_layout)
        layout.addWidget(activity_group)
        
        # Топ преподавателей
        teachers_group = QGroupBox("Топ 5 преподавателей по рейтингу")
        teachers_group.setFont(QFont("Arial", 10, QFont.Bold))
        teachers_layout = QVBoxLayout()
        
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
        
        teachers_layout.addWidget(teachers_table)
        teachers_group.setLayout(teachers_layout)
        layout.addWidget(teachers_group)
        
        # Просроченные задания
        overdue_group = QGroupBox("Студенты с просроченными заданиями")
        overdue_group.setFont(QFont("Arial", 10, QFont.Bold))
        overdue_layout = QVBoxLayout()
        
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
        
        overdue_layout.addWidget(overdue_table)
        overdue_group.setLayout(overdue_layout)
        layout.addWidget(overdue_group)
        
        
        # Прогресс студентов
        progress_group = QGroupBox("Прогресс студентов по курсам")
        progress_group.setFont(QFont("Arial", 10, QFont.Bold))
        progress_layout = QVBoxLayout()
        
        progress_table = QTableWidget()
        progress_table.setColumnCount(6)
        progress_table.setHorizontalHeaderLabels(["ID студента", "Имя", "Фамилия", "Курс", "Статус", "Дата завершения"])
        progress_table.horizontalHeader().setSectionResizeMode(QHeaderView.Stretch)
        
        progress_data = Views.get_student_progress_details()
        progress_table.setRowCount(len(progress_data))
        for row, (user_id, first_name, last_name, course_name, status, completed_at) in enumerate(progress_data):
            progress_table.setItem(row, 0, QTableWidgetItem(str(user_id)))
            progress_table.setItem(row, 1, QTableWidgetItem(first_name))
            progress_table.setItem(row, 2, QTableWidgetItem(last_name))
            progress_table.setItem(row, 3, QTableWidgetItem(course_name))
            progress_table.setItem(row, 4, QTableWidgetItem(status))
            progress_table.setItem(row, 5, QTableWidgetItem(str(completed_at) if completed_at else ""))
        
        progress_layout.addWidget(progress_table)
        progress_group.setLayout(progress_layout)
        layout.addWidget(progress_group)
        
    def create_manager_actions_section(self, layout):
        """Раздел с действиями для менеджера"""
        actions_container = QHBoxLayout()
        
        # Левая колонка с формами
        left_column = QVBoxLayout()
        
        # Управление профилями
        profile_group = QGroupBox("Управление профилями")
        profile_group.setFont(QFont("Arial", 10, QFont.Bold))
        profile_layout = QFormLayout()
        self.profile_action = QComboBox()
        self.profile_action.addItems(["INSERT", "UPDATE", "DELETE"])
        self.profile_user_id = QLineEdit()
        self.profile_first_name = QLineEdit()
        self.profile_last_name = QLineEdit()
        self.profile_email = QLineEdit()
        self.profile_password = QLineEdit()
        self.profile_role_id = QLineEdit()
        profile_btn = QPushButton("Выполнить действие")
        profile_btn.clicked.connect(self.manage_user_profile)
        profile_layout.addRow("Действие:", self.profile_action)
        profile_layout.addRow("ID пользователя:", self.profile_user_id)
        profile_layout.addRow("Имя:", self.profile_first_name)
        profile_layout.addRow("Фамилия:", self.profile_last_name)
        profile_layout.addRow("Email:", self.profile_email)
        profile_layout.addRow("Пароль (hash):", self.profile_password)
        profile_layout.addRow("ID роли:", self.profile_role_id)
        profile_layout.addRow(profile_btn)
        profile_group.setLayout(profile_layout)
        left_column.addWidget(profile_group)
        
        # Правая колонка с формами
        right_column = QVBoxLayout()
        
        # Обновление материалов
        material_group = QGroupBox("Обновление материалов")
        material_group.setFont(QFont("Arial", 10, QFont.Bold))
        material_layout = QFormLayout()
        self.material_lesson_id = QLineEdit()
        self.new_material = QTextEdit()
        material_btn = QPushButton("Обновить материалы")
        material_btn.clicked.connect(self.update_lesson_material)
        material_layout.addRow("ID урока:", self.material_lesson_id)
        material_layout.addRow("Новые материалы:", self.new_material)
        material_layout.addRow(material_btn)
        material_group.setLayout(material_layout)
        right_column.addWidget(material_group)
        
        # Создание мероприятий
        event_group = QGroupBox("Создание мероприятий")
        event_group.setFont(QFont("Arial", 10, QFont.Bold))
        event_layout = QFormLayout()
        self.event_forum_name = QLineEdit()
        self.event_course_id = QLineEdit()
        self.event_created_by = QLineEdit()
        self.event_description = QTextEdit()
        event_btn = QPushButton("Создать мероприятие")
        event_btn.clicked.connect(self.create_event)
        event_layout.addRow("Название форума:", self.event_forum_name)
        event_layout.addRow("ID курса:", self.event_course_id)
        event_layout.addRow("ID создателя:", self.event_created_by)
        event_layout.addRow("Описание:", self.event_description)
        event_layout.addRow(event_btn)
        event_group.setLayout(event_layout)
        right_column.addWidget(event_group)
        
        actions_container.addLayout(left_column)
        actions_container.addLayout(right_column)
        layout.addLayout(actions_container)

    def create_admin_tab(self):
        """Вкладка для администратора"""
        tab = QWidget()
        layout = QVBoxLayout(tab)
        
        # Создаем главный контейнер с вертикальным layout
        main_container = QVBoxLayout()
        
        # Разделы для администратора
        actions_container = QHBoxLayout()
        
        # Проверка документов
        verify_group = QGroupBox("Проверка документов")
        verify_group.setFont(QFont("Arial", 10, QFont.Bold))
        verify_layout = QFormLayout()
        self.qualification_id = QLineEdit()
        self.verified = QCheckBox("Подтверждено")
        verify_btn = QPushButton("Проверить документ")
        verify_btn.clicked.connect(self.verify_teacher_document)
        verify_layout.addRow("ID квалификации:", self.qualification_id)
        verify_layout.addRow("Статус:", self.verified)
        verify_layout.addRow(verify_btn)
        verify_group.setLayout(verify_layout)
        actions_container.addWidget(verify_group)
        
        # Изменение ролей
        role_group = QGroupBox("Изменение ролей пользователей")
        role_group.setFont(QFont("Arial", 10, QFont.Bold))
        role_layout = QFormLayout()
        self.role_user_id = QLineEdit()
        self.new_role = QLineEdit()
        role_btn = QPushButton("Изменить роль")
        role_btn.clicked.connect(self.change_user_role)
        role_layout.addRow("ID пользователя:", self.role_user_id)
        role_layout.addRow("Новая роль (ID):", self.new_role)
        role_layout.addRow(role_btn)
        role_group.setLayout(role_layout)
        actions_container.addWidget(role_group)
        
        main_container.addLayout(actions_container)
        
        # Устанавливаем главный контейнер в прокручиваемую область
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        content = QWidget()
        content.setLayout(main_container)
        scroll.setWidget(content)
        
        layout.addWidget(scroll)
        self.tabs.addTab(tab, "Администратор")
        
    def create_functions_tab(self):
        """Вкладка с функциями"""
        tab = QWidget()
        layout = QVBoxLayout(tab)
        
        # Создаем главный контейнер с вертикальным layout
        main_container = QVBoxLayout()
        
        # Разделы для функций
        actions_container = QHBoxLayout()
        
        # Левая колонка с формами
        left_column = QVBoxLayout()
        
        # Задания студента по курсу
        assignments_group = QGroupBox("Задания студента по курсу")
        assignments_group.setFont(QFont("Arial", 10, QFont.Bold))
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
        left_column.addWidget(assignments_group)
        
        # Форумы пользователя
        forums_group = QGroupBox("Форумы пользователя")
        forums_group.setFont(QFont("Arial", 10, QFont.Bold))
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
        left_column.addWidget(forums_group)
        
        # Правая колонка с формами
        right_column = QVBoxLayout()
        
        # Проверка завершения курса
        complete_group = QGroupBox("Проверка завершения курса")
        complete_group.setFont(QFont("Arial", 10, QFont.Bold))
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
        self.complete_result_label = QLabel("—")
        complete_layout.addRow("Результат:", self.complete_result_label)
        complete_group.setLayout(complete_layout)
        right_column.addWidget(complete_group)
        
        # Активность пользователя по дате
        activity_group = QGroupBox("Активность пользователя по дате")
        activity_group.setFont(QFont("Arial", 10, QFont.Bold))
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
        right_column.addWidget(activity_group)
        
        actions_container.addLayout(left_column)
        actions_container.addLayout(right_column)
        main_container.addLayout(actions_container)
        
        # Устанавливаем главный контейнер в прокручиваемую область
        scroll = QScrollArea()
        scroll.setWidgetResizable(True)
        content = QWidget()
        content.setLayout(main_container)
        scroll.setWidget(content)
        
        layout.addWidget(scroll)
        self.tabs.addTab(tab, "Функции")


    # Методы для обработки нажатий кнопок
    
    def show_teacher_rating(self):
        try:
            teacher_id = int(self.teacher_id_input.text())
            rating = Views.get_teacher_avg_rating(teacher_id)
            self.rating_result_label.setText(f"{rating:.2f}")
        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите корректный ID преподавателя")
    
    def show_teacher_load(self):
        try:
            teacher_id = int(self.teacher_load_id_input.text())
            load = Views.get_teacher_course_load(teacher_id)
            self.load_result_label.setText(str(load))
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
                self.assignments_table.setItem(row, 3, QTableWidgetItem(str(submission_date)))
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
            attachment = self.submission_attachment.text() or None
            
            success, message = Procedures.submit_assignment(
                user_id, assignment_id, submission_text, attachment
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
            file_path = self.file_url.text()
        
            # Здесь можно добавить логику для обработки файла (например, копирование в нужную папку)
            # Пока просто передаем путь к файлу
            file_url = file_path
        
            success, message = Procedures.add_teacher_qualification(
                teacher_id, qual_type, institution, description, obtained_date, file_url
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

    # Вспомогательный метод для показа сообщений
    def show_message(self, success, message):
        if success:
            QMessageBox.information(self, "Успех", message)
        else:
            QMessageBox.warning(self, "Ошибка", message)

    # Добавим новые методы для выбора файлов
def select_attachment_file(self):
    file_path, _ = QFileDialog.getOpenFileName(self, "Выберите файл для задания", "", "Все файлы (*)")
    if file_path:
        self.submission_attachment.setText(file_path)

def select_qualification_file(self):
    file_path, _ = QFileDialog.getOpenFileName(self, "Выберите файл квалификации", "", "Все файлы (*)")
    if file_path:
        self.file_url.setText(file_path)

# Изменим метод submit_assignment для работы с локальными файлами
def submit_assignment(self):
    try:
        user_id = int(self.assignment_user_id.text())
        assignment_id = int(self.assignment_id.text())
        submission_text = self.submission_text.toPlainText()
        attachment_path = self.submission_attachment.text() or None
        
        # Здесь можно добавить логику для обработки файла (например, копирование в нужную папку)
        # Пока просто передаем путь к файлу
        attachment = attachment_path
        
        success, message = Procedures.submit_assignment(
            user_id, assignment_id, submission_text, attachment
        )
        self.show_message(success, message)
    except ValueError:
        QMessageBox.warning(self, "Ошибка", "Введите корректные ID пользователя и задания")