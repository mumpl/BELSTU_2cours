import tkinter as tk
from tkinter import filedialog
from PIL import Image, ImageFilter, ImageTk, ImageEnhance

def OpenImage() :
    global img, img_label, img_tk, img_original
    file_path = filedialog.askopenfilename(
        filetypes=[("Image files", "*.jpg*;*.png;*.jpeg;*.bmp;*.gif;")])
    if file_path:
        try:
            img = Image.open(file_path)
            img_original = img.copy()
            img.thumbnail((200, 200))
            img_tk = ImageTk.PhotoImage(img)
            img_label.config(image=img_tk)
            img_label.image = img_tk
        except Exception as e:
            print("error")

def ResetImage() :
    global img, img_tk
    if img_original:
        img = img_original.copy()
        img.thumbnail((200, 200))
        img_tk = ImageTk.PhotoImage(img)
        img_label.config(image=img_tk)

def SaveImage() :
    if img:
        file_path = filedialog.asksaveasfilename(defaultextension=".png",
        filetypes=[("PNG files", "*.png"), ("JPEG files", "*.jpg;*.jpeg"), ("All files", "*.*")])
        if file_path:
            try:
                img.save(file_path)
            except Exception as e:
                print("error")

def RotateImage() :
    global img, img_label, img_tk
    if img:
        img = img.rotate(-90, expand=True)
        img_tk = ImageTk.PhotoImage(img)
        img_label.config(image=img_tk)

def GrayImage() :
    global img, img_tk
    if img:
        img = img.convert("L")
        img_tk=ImageTk.PhotoImage(img)
        img_label.config(image=img_tk)

def LightImage(value) :
    global img, img_tk
    if img_original:
        enhancer = ImageEnhance.Brightness(img_original)
        img = enhancer.enhance(float(value))
        img.thumbnail((200, 200))
        img_tk = ImageTk.PhotoImage(img)
        img_label.config(image=img_tk)

def BlurImage(value):
    global img, img_label, img_tk
    if img:
        blur_radius = float(value)
        img = img.filter(ImageFilter.GaussianBlur(blur_radius))
        img_tk = ImageTk.PhotoImage(img)
        img_label.config(image=img_tk)

def SharpenImage() :
    global img, img_tk
    if img:
        img = img.filter(ImageFilter.SHARPEN)
        img_tk = ImageTk.PhotoImage(img)
        img_label.config(image=img_tk)

root = tk.Tk()
root.title("Форма для работы с изображениями")
root.geometry("800x500")

file_path = r"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender — копия последняя\LinguaBender\images\david.png"

openButton = tk.Button(root, text="Открыть", font=("Times New Roman", 14), background="#B0E0E6", command=OpenImage)
openButton.grid(row=0, column=0, padx=30, pady=30, sticky="nw")

resetButton = tk.Button(root, text="отменить", font=("Times New Roman", 14), background="#B0E0E6", command=ResetImage)
resetButton.grid(row=0, column=1, padx=30, pady=30, sticky="nw")

saveButton = tk.Button(root, text="Сохранить", font=("Times New Roman", 14), background="#B0E0E6", command=SaveImage)
saveButton.grid(row=0, column=2, padx=30, pady=30, sticky="nw")

label = tk.Label(text="Редактирование фото", font=("Times New Roman", 16))
label.grid(row=0, column=4, padx=30, pady=20, sticky="ne")

rotateButton = tk.Button(root, text="Rotate", font=("Times New Roman", 14), background="#B0E0E6", command=RotateImage)
rotateButton.grid(row=1, column=4, padx=30, pady=20, sticky="ne")

grayButton = tk.Button(root, text="Gray", font=("Times New Roman", 14), background="#B0E0E6", command=GrayImage)
grayButton.grid(row=2, column=4, padx=30, pady=20, sticky="ne")

lightButton = tk.Scale(root, from_=0.5, to=2.0, resolution=0.1, orient=tk.HORIZONTAL, label="Light", command=LightImage)
lightButton.grid(row=3, column=4, padx=30, pady=10, sticky="ne")
lightButton.set(1.0)

blurButton = tk.Scale(root, from_=0, to=5.0, resolution=0.5, orient=tk.HORIZONTAL, label="Blur", command=BlurImage)
blurButton.grid(row=4, column=4, padx=30, pady=10, sticky="ne")
blurButton.set(0)

sharpenButton = tk.Button(root, text="Sharpen", font=("Times New Roman", 14), background="#B0E0E6", command=SharpenImage)
sharpenButton.grid(row=5, column=4, padx=30, pady=20, sticky="ne")

img_label = tk.Label(root)
img_label.grid(row=1, column=0, rowspan=3 ,columnspan=3, padx=10, pady=10)

root.mainloop()