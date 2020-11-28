from datetime import datetime


class MailBox:
    total_mailbox_quantity = 0  # визначення загального числа поштових скриньок

    def __init__(self, mailbox_max_size):
        self.mailbox_max_size = mailbox_max_size  # максимальний розмір поштової скриньки
        self.mailbox = []
        MailBox.total_mailbox_quantity += 1

    def add_mail(self, mail):
        """
        Функція додавання листів
        """
        if self.mailbox_size < self.mailbox_max_size:
            self.mailbox.append(mail)
        else:
            raise SystemExit(f"Can't add new mail, reached max mailbox size: {self.mailbox_max_size}")

    def read_mail(self, index):
        """
        Функція читання листів без видалення,
        """
        try:
            mail = self.mailbox[index]
            mail.mark_as_seen()
            return mail
        except IndexError:
            return None

    def read_and_delete_mail(self, index):
        """
        Функція читання листів з видаленням
        """
        try:
            mail = self.mailbox[index]
            mail.mark_as_seen()
            self.delete_mail(index)
            return mail
        except IndexError:
            return None

    def delete_mail(self, index):
        """
        Функція видалення заданого листа
        """
        del self.mailbox[index]

    def clear_mailbox(self):
        """
        Функція видалення усіх листів
        """
        self.mailbox = []

    @property
    def mail_quantity(self):
        """
        Повертаe кількість повідомлень
        """
        return len(self.mailbox)

    @property
    def mailbox_size(self):
        """
        Повертаe загальний розмір усіх повідомлень
        """
        return len(self.mailbox)

    def __str__(self):
        return "Mailbox. Max size {}. Current size: {}. Mail_quantity: {}".format(self.mailbox_max_size,
                                                                                  self.mailbox_size,
                                                                                  self.mail_quantity)


class Mail:
    def __init__(self, title, body, size, seen=False):
        self.title = title
        self.body = body
        self.size = size  # We should count the number of bytes for storing text. But leave as is for simplicity.
        self.seen = seen
        self.created = datetime.now(tz=None)

    def mark_as_seen(self):
        self.seen = True

    def __str__(self):
        return "Mail. Title {}. Body: {}. Size: {}. Seen{}. Created {}".format(self.title, self.body,
                                                                               self.size, self.seen, self.created)


my_mail_box = MailBox(10)
mail1 = Mail("Test", "It works!", 26)
mail2 = Mail("Test2", "Hey, how are you", 31)
print(my_mail_box) 
my_mail_box.add_mail(mail1)
my_mail_box.add_mail(mail2)
print(my_mail_box) 
m1 = my_mail_box.read_mail(0)
print(m1)  
print(MailBox.total_mailbox_quantity)  # Output: 1
my_mail_box.read_and_delete_mail(1)
print(my_mail_box) 
