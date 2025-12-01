#3. Чтение данных из файла
import pandas as pd
import numpy as np
import re
import matplotlib.pyplot as plt
import seaborn as sns

df = pd.read_csv('books.csv')

print("Имена столбцов в CSV-файле:")
print(df.columns)

#Проверка
required_columns = ['Название', 'Цена']
for col in required_columns:
    if col not in df.columns:
        print(f"Столбец '{col}' не найден в CSV-файле.")
        exit()

def clean_price(price):
    return float(re.sub(r'[^\d.]', '', price))

df['Цена'] = df['Цена'].apply(clean_price)

df_sorted = df.sort_values(by='Название')

print("Первые 5 значений:")
print(df_sorted.head())

description = df_sorted.describe(include='all')
print("\nОсновные метрики статистики:")
print(description)

grouped_df = df_sorted.groupby('Название')['Цена'].mean().reset_index()

print("\nСредняя цена повторяющегося товара (если имеются):")
print(grouped_df)

#4. Гистограмма
plt.figure(figsize=(10, 6))
sns.histplot(df['Цена'], bins=20, kde=True)
plt.title('Распределение цен на книги')
plt.xlabel('Цена (£)')
plt.ylabel('Количество')
plt.show()

#box-plot
plt.figure(figsize=(10, 6))
sns.boxplot(x=df['Цена'])
plt.title('Распределение цен на книги (Box Plot)')
plt.xlabel('Цена (£)')
plt.show()

#violin plot
plt.figure(figsize=(10, 6))
sns.violinplot(x=df['Цена'])
plt.title('Распределение цен на книги (Violin Plot)')
plt.xlabel('Цена (£)')
plt.show()