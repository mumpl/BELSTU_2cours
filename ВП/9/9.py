from wordcloud import WordCloud
from PIL import Image
import numpy as np
import matplotlib.pyplot as plt
import re

# Загружаем текст из файла
with open("D:/УЧЁБА/2 курс/ВП/9/text.txt", encoding="utf-8") as f:
    text = f.read()

text_cleaned = re.sub(r'[^\w\s]', '', text.lower())

stop_words = {
    "и", "в", "во", "не", "что", "он", "на", "я", "с", "со", "как"}

words = text_cleaned.split()
filtered_words = [word for word in words if word not in stop_words and len(word) > 2]  
filtered_text = ' '.join(filtered_words)

# Загружаем маску в форме сердечка
heart_mask = np.array(Image.open("D:/УЧЁБА/2 курс/ВП/9/mask.jpg"))  # Файл с маской сердечка

# Создаём облако слов
wc = WordCloud(
    background_color='white',
    mask=heart_mask,
    contour_width=1,
    contour_color='pink',
    colormap='spring'
).generate(filtered_text)

# Отображаем результат
plt.figure(figsize=(10, 10))
plt.imshow(wc, interpolation='bilinear')
plt.axis('off')
plt.show()

# Сохраняем в файл
wc.to_file("heart_wordcloud.png")