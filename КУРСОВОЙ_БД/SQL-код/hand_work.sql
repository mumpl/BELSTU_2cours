USE LINGUABENDER_DB;
GO

EXEC SP_TEACHER_QUALIFICATION
@teacher_id = 50,
@qualification_type = 'PhD',
@institution = 'Harvard University',
@description = 'Докторская степень по компьютерным наукам',
@obtained_date = '2020-05-15',
@file_url = 'https://example.com/certificates/phd_harvard.pdf';

