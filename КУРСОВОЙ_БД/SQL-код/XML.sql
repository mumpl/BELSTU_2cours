--импорт
SELECT post_id, thread_id, author_id, post_text, posted_at
FROM dbo.FORUM_POST
ORDER BY post_id
FOR XML PATH('Post'), ROOT('ForumPosts'), ELEMENTS;


--экспорт
USE LINGUABENDER_DB;
GO

DECLARE @xmlData XML = 
'<ForumPosts>
    <Post>
        <thread_id>10</thread_id>
        <author_id>23</author_id>
        <post_text>Пример поста</post_text>
        <posted_at>2025-04-28T12:00:00</posted_at>
    </Post>
    <Post>
        <thread_id>15</thread_id>
        <author_id>37</author_id>
        <post_text>Еще один пост</post_text>
        <posted_at>2025-04-28T12:30:00</posted_at>
    </Post>
</ForumPosts>';

DECLARE @xmlHandle INT;
EXEC sp_xml_preparedocument @xmlHandle OUTPUT, @xmlData;

INSERT INTO dbo.FORUM_POST (thread_id, author_id, post_text, posted_at)
SELECT 
    thread_id, author_id, post_text, posted_at
FROM OPENXML(@xmlHandle, '/ForumPosts/Post', 2)
WITH (
    thread_id INT 'thread_id',
    author_id INT 'author_id',
    post_text NVARCHAR(MAX) 'post_text',
    posted_at DATETIME 'posted_at'
);

EXEC sp_xml_removedocument @xmlHandle;


