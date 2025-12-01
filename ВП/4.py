import requests
from bs4 import BeautifulSoup
import csv

html = requests.get('https://books.toscrape.com/').text
soup = BeautifulSoup(html, 'html.parser')

print(soup.prettify())

books = []
for book in soup.find_all('article', class_='product_pod'):
    title = book.find('h3').find('a')['title']
    price = book.find('p', class_='price_color').text
    books.append((title, price))

for book in books:
    print(f'Название: {book[0]}, Цена: {book[1]}')

#2. работа с файлом
with open('books.csv', 'w', newline='', encoding="utf8") as file:
    writer = csv.writer(file)
    writer.writerow(['Название', 'Цена'])
    writer.writerows(books)
print("Данные записаны в файл books.csv.")


