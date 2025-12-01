from sqlalchemy import text
from database import SessionLocal

class Views:
    
#ПРЕДСТАВЛЕНИЯ   ПРЕДСТАВЛЕНИЯ   ПРЕДСТАВЛЕНИЯ
    @staticmethod
    def get_lesson_records():
        with SessionLocal() as session:
            query = text("""
                SELECT u.user_id, l.lesson_name, l.lesson_materials
                FROM [USER] u
                JOIN USER_COURSE uc ON u.user_id = uc.user_id
                JOIN COURSE c ON uc.course_id = c.course_id
                JOIN LESSON l ON c.course_id = l.course_id
                WHERE u.role_id = (SELECT role_id FROM [ROLE] WHERE role_name = 'Студент')
            """)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_user_activity_sum():
        with SessionLocal() as session:
            query = text("""
                SELECT u.user_id, u.first_name, u.last_name, COUNT(ua.activity_id) AS activity_count
                FROM [USER] u
                LEFT JOIN USER_ACTIVITY ua ON u.user_id = ua.user_id
                GROUP BY u.user_id, u.first_name, u.last_name
            """)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_top_teachers_by_rating():
        with SessionLocal() as session:
            query = text("""
                SELECT TOP 5
                u.user_id, u.first_name, u.last_name,
                AVG(tr.rating) AS average_rating
                from TEACHER_RATING tr
                JOIN [USER] u ON tr.teacher_id = u.user_id
                GROUP BY u.user_id, u.first_name, u.last_name
                ORDER BY average_rating desc
            """)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_students_with_overdue_assignments():
        with SessionLocal() as session:
            query = text("""
                SELECT 
                    u.user_id,
                    u.first_name,
                    u.last_name,
                    a.assignment_id,
                    a.due_date,
                    ua.submission_date
                FROM USER_ASSIGNMENT ua
                JOIN ASSIGNMENT a ON ua.assignment_id = a.assignment_id
                JOIN [USER] u ON ua.user_id = u.user_id
                WHERE ua.submission_date > a.due_date
            """)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_teacher_schedule_week():
        with SessionLocal() as session:
            query = text("""
                SELECT 
                    u.user_id,
                    u.first_name,
                    u.last_name,
                    c.course_name,
                    l.lesson_name,
                    s.schedule_date,
                    s.start_time,
                    s.end_time
                FROM SCHEDULE s
                JOIN LESSON l ON s.lesson_id = l.lesson_id
                JOIN COURSE c ON l.course_id = c.course_id
                JOIN [USER] u ON c.teacher_id = u.user_id
                WHERE s.schedule_date BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE())
            """)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_student_progress_details():
        with SessionLocal() as session:
            query = text("""
                SELECT 
                    u.user_id,
                    u.first_name,
                    u.last_name,
                    c.course_name,
                    cp.status,
                    cp.completed_at
                FROM COURSE_PROGRESS cp
                JOIN [USER] u ON cp.user_id = u.user_id
                JOIN COURSE c ON cp.course_id = c.course_id
            """)
            result = session.execute(query)
            return result.fetchall()
        
    @staticmethod
    def get_student_materials():
        try:
            with SessionLocal() as session:
            # Проверка наличия данных в таблицах
                user_count = session.execute(text("SELECT COUNT(*) FROM [USER]")).scalar()
                role_count = session.execute(text("SELECT COUNT(*) FROM [ROLE]")).scalar()
                print(f"Total users: {user_count}, roles: {role_count}")
            
            # Проверка наличия роли 'Студент'
                student_role_id = session.execute(
                    text("SELECT role_id FROM [ROLE] WHERE role_name = 'Студент'")
                ).scalar()
                print(f"Student role ID: {student_role_id}")
            
                if not student_role_id:
                    print("Role 'Студент' not found in database!")
                    return []

                query = text("""
                    SELECT u.user_id, c.course_name, l.lesson_name, l.lesson_materials
                    FROM [USER] u
                    JOIN USER_COURSE uc ON u.user_id = uc.user_id
                    JOIN COURSE c ON uc.course_id = c.course_id
                    JOIN LESSON l ON c.course_id = l.course_id
                    WHERE u.role_id = :role_id
                """).bindparams(role_id=student_role_id)
            
                result = session.execute(query)
                data = result.fetchall()
            
                print(f"Found {len(data)} records:")
                for row in data:
                    print(row)
                
                return data
        except Exception as e:
            print("Error in get_student_materials:", str(e))
            return []
        
#ФУНКЦИИ   ФУНКЦИИ   ФУНКЦИИ        
    @staticmethod
    def get_teacher_avg_rating(teacher_id):
        with SessionLocal() as session:
            query = text("SELECT dbo.FN_TEACHER_AV_RATING(:teacher_id)").bindparams(teacher_id=teacher_id)
            result = session.execute(query)
            return result.scalar()

    @staticmethod
    def get_student_assignments_by_course(user_id, course_id):
        with SessionLocal() as session:
            query = text("""
                SELECT * FROM dbo.FN_STUD_ASSIGNMNET_BY_COURSE(:user_id, :course_id)
            """).bindparams(user_id=user_id, course_id=course_id)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_user_forums(user_id):
        with SessionLocal() as session:
            query = text("SELECT * FROM dbo.FN_USER_FORUMS(:user_id)").bindparams(user_id=user_id)
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def is_course_completed(user_id, course_id):
        with SessionLocal() as session:
            query = text("SELECT dbo.FN_COURSE_COMPLETED(:user_id, :course_id)").bindparams(
                user_id=user_id, course_id=course_id)
            result = session.execute(query)
            return bool(result.scalar())

    @staticmethod
    def get_completed_courses_count(user_id):
        with SessionLocal() as session:
            query = text("SELECT dbo.FN_COMPLETED_COURSES_COUNT(:user_id)").bindparams(user_id=user_id)
            result = session.execute(query)
            return result.scalar()

    @staticmethod
    def get_teacher_course_load(teacher_id):
        with SessionLocal() as session:
            query = text("SELECT dbo.FN_TEACHER_COURSE_LOAD(:teacher_id)").bindparams(teacher_id=teacher_id)
            result = session.execute(query)
            return result.scalar()

    @staticmethod
    def get_student_avg_grade(user_id):
        with SessionLocal() as session:
            query = text("SELECT dbo.FN_STUDENT_AVG_GRADE(:user_id)").bindparams(user_id=user_id)
            result = session.execute(query)
            return result.scalar()

    @staticmethod
    def get_top_courses_by_student_count():
        with SessionLocal() as session:
            query = text("SELECT * FROM dbo.FN_TOP_COURSES_BY_STUD_COUNT()")
            result = session.execute(query)
            return result.fetchall()

    @staticmethod
    def get_user_activity_by_date(user_id, activity_date):
        with SessionLocal() as session:
            query = text("SELECT * FROM dbo.FN_USER_ACTIVITY_BY_DATE(:user_id, :activity_date)").bindparams(
                user_id=user_id, activity_date=activity_date)
            result = session.execute(query)
            return result.fetchall()
        
