# Markdown File

Conversion fractions sourced from https://unitchefs.com/

To add a new unit type XXX:

1. Add a sealed XXXUnit class that inherits from Unit
2. Add unit definition(s) as public static read only fields
3. Add a SI reference if applicable
4. Add a reference to XXXUnit in UnitRegistration
5. Add unit tests

To add a new unit value to an existing unit type:
1. Add public static read only field with unit definition in corresponding class
2. Add unit tests

Where possible, base factors on the SI unit and use fractions for higher precision.