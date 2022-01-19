from kivy.app import App
from kivy.uix.widget import Widget
from kivy.properties import ObjectProperty
from kivy.lang import Builder
from kivy.core.window import Window

# Set the app size
Window.size = (500,700)

# Designate Our .kv design file 
Builder.load_file('calc.kv')


class MyLayout(Widget):
	egyszam = 0.0
	muvelet = ""

	def clear(self):
		self.ids.calc_input.text = '0'

	def ce(self):
		self.ids.calc_input.text = '0'

	def szazalek(self):
		a = float(self.ids.calc_input.text)
		a = a / 100
		self.ids.calc_input.text = str(a)

	def nulla(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 0
		self.ids.calc_input.text = str(a)

	def egy(self):
		a= float(self.ids.calc_input.text)
		a = a*10+1
		self.ids.calc_input.text = str(a)

	def ketto(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 2
		self.ids.calc_input.text = str(a)

	def harom(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 3
		self.ids.calc_input.text = str(a)

	def negy(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 4
		self.ids.calc_input.text = str(a)

	def ot(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 5
		self.ids.calc_input.text = str(a)

	def hat(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 6
		self.ids.calc_input.text = str(a)

	def het(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 7
		self.ids.calc_input.text = str(a)

	def nyolc(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 8
		self.ids.calc_input.text = str(a)

	def kilenc(self):
		a = float(self.ids.calc_input.text)
		a = a * 10 + 9
		self.ids.calc_input.text = str(a)

	def plusminus(self):
		a = float(self.ids.calc_input.text)
		a = a * -1
		self.ids.calc_input.text = str(a)

	def plus(self):
		self.egyszam = float(self.ids.calc_input.text)
		self.muvelet = "plus"
		self.ids.calc_input.text = "0"


	def minus(self):
		self.egyszam = float(self.ids.calc_input.text)
		self.muvelet = "minus"
		self.ids.calc_input.text = "0"

	def szor(self):
		self.egyszam = float(self.ids.calc_input.text)
		self.muvelet = "szor"
		self.ids.calc_input.text = "0"

	def per(self):
		self.egyszam = float(self.ids.calc_input.text)
		self.muvelet = "per"
		self.ids.calc_input.text = "0"

	def egyenlo(self):

		if self.muvelet == "plus":
			a = self.egyszam + float(self.ids.calc_input.text)
			print(self.egyszam,"+", float(self.ids.calc_input.text))
			self.ids.calc_input.text = str(a)

		elif self.muvelet == "minus":
			a = self.egyszam - float(self.ids.calc_input.text)
			print(self.egyszam,"-", float(self.ids.calc_input.text))
			self.ids.calc_input.text = str(a)

		elif self.muvelet == "szor":
			a = self.egyszam * float(self.ids.calc_input.text)
			print(self.egyszam,"*", float(self.ids.calc_input.text))
			self.ids.calc_input.text = str(a)

		elif self.muvelet == "per":
			a = self.egyszam / float(self.ids.calc_input.text)
			print(self.egyszam,"/", float(self.ids.calc_input.text))
			self.ids.calc_input.text = str(a)




class CalculatorApp(App):
	def build(self):
		return MyLayout()

if __name__ == '__main__':
	CalculatorApp().run()