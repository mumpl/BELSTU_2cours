# game.py
import pygame
import random
import os

# Инициализация
pygame.init()
pygame.mixer.init() #для работы со звуком

# Настройки окна
WIDTH, HEIGHT = 550, 700
WIN = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("The Cat's Serenade")  

# Цвета
WHITE = (255, 255, 255)
BLACK = (0, 0, 0) 

# Загрузка ассетов
ASSET_DIR = "assets"
PLAYER_IMG = pygame.image.load(os.path.join(ASSET_DIR, "player.png"))
STAR_IMG = pygame.image.load(os.path.join(ASSET_DIR, "nota.png"))
BACKGROUND_IMG = pygame.image.load(os.path.join(ASSET_DIR, "fon2.jpg"))
BACKGROUND_IMG = pygame.transform.scale(BACKGROUND_IMG, (WIDTH, HEIGHT))

CATCH_SOUND = pygame.mixer.Sound(os.path.join(ASSET_DIR, "catch.wav"))
MISS_SOUND = pygame.mixer.Sound(os.path.join(ASSET_DIR, "miss.wav"))

FONT = pygame.font.SysFont("arial", 32)
BIG_FONT = pygame.font.SysFont("arial", 64)

# Классы
class Player(pygame.sprite.Sprite):
    def __init__(self):
        super().__init__()
        self.image = pygame.transform.scale(PLAYER_IMG, (100, 100))
        self.rect = self.image.get_rect()
        self.rect.centerx = WIDTH // 2
        self.rect.bottom = HEIGHT - 10
        self.speed = 7

    def update(self, keys):
        if keys[pygame.K_LEFT] and self.rect.left > 0:
            self.rect.x -= self.speed
        if keys[pygame.K_RIGHT] and self.rect.right < WIDTH:
            self.rect.x += self.speed

class Star(pygame.sprite.Sprite):
    def __init__(self):
        super().__init__()
        self.image = pygame.transform.scale(STAR_IMG, (50, 50))
        self.rect = self.image.get_rect()
        self.rect.x = random.randint(0, WIDTH - self.rect.width)
        self.rect.y = random.randint(-100, -40)
        self.speed = random.randint(3, 7)

    def update(self):
        self.rect.y += self.speed
        if self.rect.top > HEIGHT:
            self.kill()
            MISS_SOUND.play()
            game_state["lives"] -= 1

# Игровое состояние
def reset_game():
    game_state["score"] = 0
    game_state["lives"] = 5
    all_sprites.empty()
    stars.empty()
    player = Player()
    all_sprites.add(player)
    return player

game_state = {
    "score": 0,
    "lives": 5
}

# Группы спрайтов
all_sprites = pygame.sprite.Group()
stars = pygame.sprite.Group()
player = reset_game()

# Игровой цикл
clock = pygame.time.Clock()
running = True
game_over = False
SPAWN_STAR_EVENT = pygame.USEREVENT + 1 
pygame.time.set_timer(SPAWN_STAR_EVENT, 1000)  # каждую секунду новая нота

def draw_ui():      # отображение очков и жизней
    score_text = FONT.render(f"Score: {game_state['score']}", True, WHITE)
    lives_text = FONT.render(f"Lives: {game_state['lives']}", True, WHITE)
    WIN.blit(score_text, (10, 10))                                         # позиция текста
    WIN.blit(lives_text, (WIDTH - lives_text.get_width() - 10, 10))

def show_game_over():
    WIN.blit(BACKGROUND_IMG, (0, 0))
    game_over_text = BIG_FONT.render("GAME OVER", True, WHITE)
    restart_text = FONT.render("Press [R] to Restart or [Q] to Quit", True, WHITE)
    WIN.blit(game_over_text, ((WIDTH - game_over_text.get_width()) // 2, HEIGHT // 2 - 50))
    WIN.blit(restart_text, ((WIDTH - restart_text.get_width()) // 2, HEIGHT // 2 + 20))
    pygame.display.flip()  # обновляет экран, чтобы изменения стали видимыми.

while running:
    clock.tick(60)   #частота обновления экрана 60 кадров в секунду
    keys = pygame.key.get_pressed()

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

        if game_over:
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_r:
                    game_over = False
                    player = reset_game()
                elif event.key == pygame.K_q:
                    running = False
        else:
            if event.type == SPAWN_STAR_EVENT:
                star = Star()
                all_sprites.add(star)
                stars.add(star)

    if not game_over:
        player.update(keys)
        stars.update()

        hits = pygame.sprite.spritecollide(player, stars, True)
        for hit in hits:
            game_state["score"] += 1
            CATCH_SOUND.play()

        if game_state["lives"] <= 0:
            game_over = True

        WIN.blit(BACKGROUND_IMG, (0, 0))
        all_sprites.draw(WIN)
        draw_ui()
        pygame.display.flip()
    else:
        show_game_over()

pygame.quit()
