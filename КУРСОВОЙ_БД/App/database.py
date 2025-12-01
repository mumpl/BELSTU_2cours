from sqlalchemy import create_engine, Column, Integer, String, DateTime, Date, Time, ForeignKey, Text, Float, func, cast
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker, relationship
from sqlalchemy.sql import expression
from sqlalchemy.ext.compiler import compiles
from sqlalchemy.types import UserDefinedType
from sqlalchemy.sql import text
import datetime

# Подключение к базе данных
DATABASE_URL = "mssql+pyodbc://LAPTOP/LINGUABENDER_DB?trusted_connection=yes&driver=ODBC+Driver+17+for+SQL+Server"
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

Base = declarative_base()

# Класс для работы с пользовательскими функциями SQL Server
class FloatFunction(UserDefinedType):
    def get_col_spec(self, **kw):
        return "FLOAT"

class BitFunction(UserDefinedType):
    def get_col_spec(self, **kw):
        return "BIT"

class IntFunction(UserDefinedType):
    def get_col_spec(self, **kw):
        return "INT"

class Role(Base):
    __tablename__ = 'ROLE'
    role_id = Column(Integer, primary_key=True, autoincrement=True)
    role_name = Column(String(50))
    description = Column(String(200))
    users = relationship("User", back_populates="role")

class User(Base):
    __tablename__ = 'USER'
    user_id = Column(Integer, primary_key=True, autoincrement=True)
    first_name = Column(String(50))
    last_name = Column(String(50))
    email = Column(String(50), unique=True)
    password_hash = Column(String(50))
    role_id = Column(Integer, ForeignKey('ROLE.role_id'))
    created_at = Column(DateTime, default=datetime.datetime.utcnow)
    
    role = relationship("Role", back_populates="users")
    taught_courses = relationship("Course", back_populates="teacher")
    user_courses = relationship("UserCourse", back_populates="user")
    certificates = relationship("Certificate", back_populates="user")
    course_progresses = relationship("CourseProgress", back_populates="user")
    user_assignments = relationship("UserAssignment", back_populates="user")
    activities = relationship("UserActivity", back_populates="user")
    course_reviews = relationship("CourseReview", back_populates="user")
    qualifications = relationship("TeacherQualification", back_populates="teacher")
    ratings_received = relationship("TeacherRating", foreign_keys="[TeacherRating.teacher_id]", back_populates="teacher")
    ratings_given = relationship("TeacherRating", foreign_keys="[TeacherRating.reviewer_id]", back_populates="reviewer")
    created_forums = relationship("Forum", back_populates="creator")
    created_threads = relationship("ForumThread", back_populates="creator")
    forum_posts = relationship("ForumPost", back_populates="author")

class Course(Base):
    __tablename__ = 'COURSE'
    course_id = Column(Integer, primary_key=True, autoincrement=True)
    course_name = Column(String(50))
    description = Column(String(200))
    start_date = Column(Date)
    end_date = Column(Date)
    teacher_id = Column(Integer, ForeignKey('USER.user_id'))
    
    teacher = relationship("User", back_populates="taught_courses")
    lessons = relationship("Lesson", back_populates="course")
    user_courses = relationship("UserCourse", back_populates="course")
    certificates = relationship("Certificate", back_populates="course")
    course_progresses = relationship("CourseProgress", back_populates="course")
    reviews = relationship("CourseReview", back_populates="course")
    schedules = relationship("Schedule", back_populates="course")
    forums = relationship("Forum", back_populates="course")

class Lesson(Base):
    __tablename__ = 'LESSON'
    lesson_id = Column(Integer, primary_key=True, autoincrement=True)
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    lesson_name = Column(String(50))
    lesson_date = Column(Date)
    lesson_duration = Column(Time)
    lesson_materials = Column(String(200))
    
    course = relationship("Course", back_populates="lessons")
    assignments = relationship("Assignment", back_populates="lesson")
    schedules = relationship("Schedule", back_populates="lesson")

class UserCourse(Base):
    __tablename__ = 'USER_COURSE'
    user_course_id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey('USER.user_id'))
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    enrollment_date = Column(Date)
    
    user = relationship("User", back_populates="user_courses")
    course = relationship("Course", back_populates="user_courses")

class Certificate(Base):
    __tablename__ = 'CERTIFICATES'
    sertificate_id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey('USER.user_id'))
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    course_name = Column(String(50))
    issue_date = Column(Date)
    
    user = relationship("User", back_populates="certificates")
    course = relationship("Course", back_populates="certificates")

class CourseProgress(Base):
    __tablename__ = 'COURSE_PROGRESS'
    progress_id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey('USER.user_id'))
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    status = Column(String(50))
    completed_at = Column(DateTime)
    
    user = relationship("User", back_populates="course_progresses")
    course = relationship("Course", back_populates="course_progresses")

class Assignment(Base):
    __tablename__ = 'ASSIGNMENT'
    assignment_id = Column(Integer, primary_key=True, autoincrement=True)
    lesson_id = Column(Integer, ForeignKey('LESSON.lesson_id'))
    description = Column(String(200))
    due_date = Column(Date)
    
    lesson = relationship("Lesson", back_populates="assignments")
    user_assignments = relationship("UserAssignment", back_populates="assignment")

class UserAssignment(Base):
    __tablename__ = 'USER_ASSIGNMENT'
    user_assignment_id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey('USER.user_id'))
    assignment_id = Column(Integer, ForeignKey('ASSIGNMENT.assignment_id'))
    submission_text = Column(String(200))
    attachment = Column(Text)
    submission_date = Column(DateTime)
    grade = Column(Integer)
    feedback = Column(String(200))
    
    user = relationship("User", back_populates="user_assignments")
    assignment = relationship("Assignment", back_populates="user_assignments")

class UserActivity(Base):
    __tablename__ = 'USER_ACTIVITY'
    activity_id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey('USER.user_id'))
    activity_description = Column(String(50))
    activity_datetime = Column(DateTime)
    
    user = relationship("User", back_populates="activities")

class CourseReview(Base):
    __tablename__ = 'COURSE_REVIEW'
    review_id = Column(Integer, primary_key=True, autoincrement=True)
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    user_id = Column(Integer, ForeignKey('USER.user_id'))
    rating = Column(Integer)
    review_text = Column(String(200))
    review_date = Column(DateTime)
    
    course = relationship("Course", back_populates="reviews")
    user = relationship("User", back_populates="course_reviews")

class Schedule(Base):
    __tablename__ = 'SCHEDULE'
    schedule_id = Column(Integer, primary_key=True, autoincrement=True)
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    lesson_id = Column(Integer, ForeignKey('LESSON.lesson_id'))
    start_time = Column(Time)
    end_time = Column(Time)
    day_of_week = Column(Integer)
    schedule_date = Column(Date)
    
    course = relationship("Course", back_populates="schedules")
    lesson = relationship("Lesson", back_populates="schedules")
    exception_dates = relationship("ExceptionDate", back_populates="schedule")

class ExceptionDate(Base):
    __tablename__ = 'EXCEPTION_DATE'
    exception_id = Column(Integer, primary_key=True, autoincrement=True)
    schedule_id = Column(Integer, ForeignKey('SCHEDULE.schedule_id'))
    exception_date = Column(Date)
    reason = Column(String(100))
    rescheduling_for = Column(Date)
    
    schedule = relationship("Schedule", back_populates="exception_dates")

class TeacherQualification(Base):
    __tablename__ = 'TEACHER_QUALIFICATION'
    qualification_id = Column(Integer, primary_key=True, autoincrement=True)
    teacher_id = Column(Integer, ForeignKey('USER.user_id'))
    qualification_type = Column(String(50))
    institution = Column(String(50))
    description = Column(String(200))
    obtained_date = Column(Date)
    file_url = Column(String(500))
    
    teacher = relationship("User", back_populates="qualifications")

class TeacherRating(Base):
    __tablename__ = 'TEACHER_RATING'
    rating_id = Column(Integer, primary_key=True, autoincrement=True)
    teacher_id = Column(Integer, ForeignKey('USER.user_id'))
    rating = Column(Integer)
    review = Column(String(200))
    reviewer_id = Column(Integer, ForeignKey('USER.user_id'))
    review_date = Column(DateTime)
    
    teacher = relationship("User", foreign_keys=[teacher_id], back_populates="ratings_received")
    reviewer = relationship("User", foreign_keys=[reviewer_id], back_populates="ratings_given")

class Forum(Base):
    __tablename__ = 'FORUM'
    forum_id = Column(Integer, primary_key=True, autoincrement=True)
    course_id = Column(Integer, ForeignKey('COURSE.course_id'))
    forum_name = Column(String(50))
    created_by = Column(Integer, ForeignKey('USER.user_id'))
    created_at = Column(DateTime)
    description = Column(String(200))
    
    course = relationship("Course", back_populates="forums")
    creator = relationship("User", back_populates="created_forums")
    threads = relationship("ForumThread", back_populates="forum")

class ForumThread(Base):
    __tablename__ = 'FORUM_THREAD'
    thread_id = Column(Integer, primary_key=True, autoincrement=True)
    forum_id = Column(Integer, ForeignKey('FORUM.forum_id'))
    thread_title = Column(String(50))
    created_by = Column(Integer, ForeignKey('USER.user_id'))
    created_at = Column(DateTime)
    
    forum = relationship("Forum", back_populates="threads")
    creator = relationship("User", back_populates="created_threads")
    posts = relationship("ForumPost", back_populates="thread")

class ForumPost(Base):
    __tablename__ = 'FORUM_POST'
    post_id = Column(Integer, primary_key=True, autoincrement=True)
    thread_id = Column(Integer, ForeignKey('FORUM_THREAD.thread_id'))
    author_id = Column(Integer, ForeignKey('USER.user_id'))
    post_text = Column(String(200))
    posted_at = Column(DateTime)
    
    thread = relationship("ForumThread", back_populates="posts")
    author = relationship("User", back_populates="forum_posts")

class Procedures:
    @staticmethod
    def join_lesson(user_id: int, lesson_id: int):
        """Процедура SP_JOIN_LESSON - участие студента в уроке"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_JOIN_LESSON :user_id, :lesson_id"),
                    {"user_id": user_id, "lesson_id": lesson_id}
                )
                session.commit()
                return True, "Успешно присоединились к уроку"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def submit_assignment(user_id: int, assignment_id: int, submission_text: str, attachment: str = None):
        """Процедура SP_SUBMIT_ASSIGNMENT - отправка задания"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_SUBMIT_ASSIGNMENT :user_id, :assignment_id, :submission_text, :attachment"),
                    {
                        "user_id": user_id,
                        "assignment_id": assignment_id,
                        "submission_text": submission_text,
                        "attachment": attachment
                    }
                )
                session.commit()
                return True, "Задание успешно отправлено"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def post_forum_message(user_id: int, thread_id: int, post_text: str):
        """Процедура SP_POST_FORUM_MESSAGE - отправка сообщения на форум"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_POST_FORUM_MESSAGE :user_id, :thread_id, :post_text"),
                    {
                        "user_id": user_id,
                        "thread_id": thread_id,
                        "post_text": post_text
                    }
                )
                session.commit()
                return True, "Сообщение успешно отправлено"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def update_user_info(user_id: int, first_name: str, last_name: str, email: str):
        """Процедура SP_UPDATE_USER_INFO - обновление информации пользователя"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_UPDATE_USER_INFO :user_id, :first_name, :last_name, :email"),
                    {
                        "user_id": user_id,
                        "first_name": first_name,
                        "last_name": last_name,
                        "email": email
                    }
                )
                session.commit()
                return True, "Информация успешно обновлена"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def issue_certificate(user_id: int, course_id: int):
        """Процедура SP_ISSUE_CERTIFICATE - выдача сертификата"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_ISSUE_CERTIFICATE :user_id, :course_id"),
                    {"user_id": user_id, "course_id": course_id}
                )
                session.commit()
                return True, "Сертификат успешно выдан"
            except Exception as e:
                session.rollback()
                return False, str(e)

    # Процедуры преподавателя
    @staticmethod
    def add_teacher_qualification(teacher_id: int, qualification_type: str, institution: str, 
                                description: str, obtained_date: str, file_url: str):
        """Процедура SP_TEACHER_QUALIFICATION - добавление квалификации"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_TEACHER_QUALIFICATION :teacher_id, :qualification_type, :institution, "
                        ":description, :obtained_date, :file_url"),
                    {
                        "teacher_id": teacher_id,
                        "qualification_type": qualification_type,
                        "institution": institution,
                        "description": description,
                        "obtained_date": obtained_date,
                        "file_url": file_url
                    }
                )
                session.commit()
                return True, "Квалификация успешно добавлена"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def create_course(course_name: str, description: str, start_date: str, 
                     end_date: str, teacher_id: int):
        """Процедура SP_CREATE_COURSE - создание курса"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_CREATE_COURSE :course_name, :description, :start_date, "
                        ":end_date, :teacher_id"),
                    {
                        "course_name": course_name,
                        "description": description,
                        "start_date": start_date,
                        "end_date": end_date,
                        "teacher_id": teacher_id
                    }
                )
                session.commit()
                return True, "Курс успешно создан"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def schedule_lesson(course_id: int, lesson_name: str, lesson_date: str, 
                       lesson_duration: str, lesson_materials: str):
        """Процедура SP_SCHEDULE_LESSON - планирование урока"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_SCHEDULE_LESSON :course_id, :lesson_name, :lesson_date, "
                        ":lesson_duration, :lesson_materials"),
                    {
                        "course_id": course_id,
                        "lesson_name": lesson_name,
                        "lesson_date": lesson_date,
                        "lesson_duration": lesson_duration,
                        "lesson_materials": lesson_materials
                    }
                )
                session.commit()
                return True, "Урок успешно запланирован"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def grade_assignment(user_assignment_id: int, grade: int, feedback: str):
        """Процедура SP_GRADE_ASSIGNMENT - оценка задания"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_GRADE_ASSIGNMENT :user_assignment_id, :grade, :feedback"),
                    {
                        "user_assignment_id": user_assignment_id,
                        "grade": grade,
                        "feedback": feedback
                    }
                )
                session.commit()
                return True, "Оценка успешно выставлена"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def manage_schedule(course_id: int, lesson_id: int, start_time: str, 
                       end_time: str, day_of_week: int, schedule_date: str):
        """Процедура SP_MANAGE_SCHEDULE - управление расписанием"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_MANAGE_SCHEDULE :course_id, :lesson_id, :start_time, "
                        ":end_time, :day_of_week, :schedule_date"),
                    {
                        "course_id": course_id,
                        "lesson_id": lesson_id,
                        "start_time": start_time,
                        "end_time": end_time,
                        "day_of_week": day_of_week,
                        "schedule_date": schedule_date
                    }
                )
                session.commit()
                return True, "Расписание успешно обновлено"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def log_teacher_training(teacher_id: int, event_description: str):
        """Процедура SP_LOG_TEACHER_TRAINING - логирование обучения преподавателя"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_LOG_TEACHER_TRAINING :teacher_id, :event_description"),
                    {"teacher_id": teacher_id, "event_description": event_description}
                )
                session.commit()
                return True, "Обучение успешно залогировано"
            except Exception as e:
                session.rollback()
                return False, str(e)

    # Процедуры менеджера
    @staticmethod
    def manage_user_profile(action: str, user_id: int = None, first_name: str = None, 
                          last_name: str = None, email: str = None, 
                          password_hash: str = None, role_id: int = None):
        """Процедура SP_MANAGE_USER_PROFILE - управление профилями пользователей"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_MANAGE_USER_PROFILE :action, :user_id, :first_name, "
                        ":last_name, :email, :password_hash, :role_id"),
                    {
                        "action": action,
                        "user_id": user_id,
                        "first_name": first_name,
                        "last_name": last_name,
                        "email": email,
                        "password_hash": password_hash,
                        "role_id": role_id
                    }
                )
                session.commit()
                return True, "Операция с профилем успешно выполнена"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def update_lesson_material(lesson_id: int, new_material: str):
        """Процедура SP_UPDATE_LESSON_MATERIAL - обновление материалов урока"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_UPDATE_LESSON_MATERIAL :lesson_id, :new_material"),
                    {"lesson_id": lesson_id, "new_material": new_material}
                )
                session.commit()
                return True, "Материалы урока успешно обновлены"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def create_event(forum_name: str, course_id: int, created_by: int, description: str):
        """Процедура SP_CREATE_EVENT - создание мероприятия"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_CREATE_EVENT :forum_name, :course_id, :created_by, :description"),
                    {
                        "forum_name": forum_name,
                        "course_id": course_id,
                        "created_by": created_by,
                        "description": description
                    }
                )
                session.commit()
                return True, "Мероприятие успешно создано"
            except Exception as e:
                session.rollback()
                return False, str(e)

    # Процедуры администратора
    @staticmethod
    def verify_teacher_document(qualification_id: int, verified: bool):
        """Процедура SP_VERIFY_TEACHER_DOCUMENT - проверка документов преподавателя"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_VERIFY_TEACHER_DOCUMENT :qualification_id, :verified"),
                    {"qualification_id": qualification_id, "verified": int(verified)}
                )
                session.commit()
                return True, "Статус проверки документа обновлен"
            except Exception as e:
                session.rollback()
                return False, str(e)

    @staticmethod
    def change_user_role(user_id: int, new_role: int):
        """Процедура SP_CHANGE_USER_ROLE - изменение роли пользователя"""
        with SessionLocal() as session:
            try:
                session.execute(
                    text("EXEC SP_CHANGE_USER_ROLE :user_id, :new_role"),
                    {"user_id": user_id, "new_role": new_role}
                )
                session.commit()
                return True, "Роль пользователя успешно изменена"
            except Exception as e:
                session.rollback()
                return False, str(e)

# Создание всех таблиц в базе данных
Base.metadata.create_all(bind=engine)