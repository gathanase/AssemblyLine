import json
import sys

with open("Pocket-Recipes.json") as f:
    pocketData = json.load(f)

def rename(artifactType):
    return artifactType.upper().replace(' ', '_')

recipes = []
for outputType, pocketRecipe in pocketData["Recipe"].items():
    if outputType == "(None)": continue
    inputs = [{"type": rename(req["name"]), "quantity": req["quantity"]} for req in pocketRecipe["Requirements"]]
    recipe = {"output": rename(outputType), "inputs": inputs}
    recipes.append(recipe)

with open("Recipes.json", "w") as f:
    json.dump({"recipes": recipes}, f, indent=2)


