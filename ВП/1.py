a = int(input("input a="))
b = float(input("input b="))
print(a+b)
print(a-b)
print(a*b)
print(a/b)
print(round((a/b),2))
if (a % 2) == 0:
    print("чётное")
else:
    print("нечётное")

str = str(input("введите строку "))
print(len(str))
print(str.upper())
isdigit = any(char.isdigit() for char in str)
print(isdigit)
second = len(str)//2
secpart = str[second:]
print(secpart)

list = [1, 12, 9, 5, 15, 12]
print(list[5])
list.append(14)
print(list)
list_count = list.count(12)
print(list_count)
list.sort()
print(list)

sentence = input("input sentence: ")
words = sentence.split()
word_count = len(words)
print(word_count)

for i in range(11,23):
    square = i*i
print(i, square)

