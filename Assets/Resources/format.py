import json
import sys

with open("Pocket-Recipes.json") as f:
    pocketData = json.load(f)

def rename(artifactType):
    return artifactType.upper().replace(' ', '_')

recipes = []
for output, pocketRecipe in pocketData["Recipe"].items():
    if output == "(None)": continue
    inputs = {rename(req["name"]): req["quantity"] for req in pocketRecipe["Requirements"]}
    recipe = {"output": rename(output), "inputs": inputs}
    recipes.append(recipe)

with open("Recipes.json", "w") as f:
    json.dump(recipes, f, indent=2)


