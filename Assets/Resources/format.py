import json
import sys

def formatRecipes():
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

def formatMachines():
    with open("Pocket-Machines.json") as f:
        pocketData = json.load(f)
    
    def rename(machineType):
        return machineType.upper().replace(' ', '_')
    
    machines = []
    for machineType, pocketMachine in pocketData.items():
        machineType = rename(machineType)
        if machineType in ["FILTERED_ROBOTIC_ARM", "LEFT_SELECTOR", "LEFT_SPLITTER", "MULTI_SELECTOR", "RIGHT_SELECTOR", "RIGHT_SPLITTER", "ROBOTIC_ARM", "SELECTOR", "SPLITTER", "TRANSPORTER_INPUT", "TRANSPORTER_OUTPUT"]: continue
        machine = {
          "type": machineType,
          "name": pocketMachine["MachineName"],
          "cost": pocketMachine["BuildCost"]
        }
        machines.append(machine)
    
    with open("Machines.json", "w") as f:
        json.dump({"machines": machines}, f, indent=2)

def formatArtifacts():
    with open("Pocket-Artifacts.json") as f:
        pocketData = json.load(f)
    
    def rename(artifactType):
        return artifactType.upper().replace(' ', '_')
    
    artifacts = []
    for artifactType, pocketArtifact in pocketData.items():
        if artifactType == "(None)": continue
        artifact = {
          "type": rename(artifactType),
          "name": pocketArtifact["name"],
          "cost": pocketArtifact["value"]
        }
        artifacts.append(artifact)
    
    with open("Artifacts.json", "w") as f:
        json.dump({"artifacts": artifacts}, f, indent=2)


formatRecipes()
formatMachines()
formatArtifacts()
