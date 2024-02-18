#!/usr/bin/python3

import os
import subprocess
from subprocess import Popen
from shutil import which


def recurse_files(path: str, files: list[str]):
    for file in os.listdir(path):
        full_path = os.path.join(path, file)

        if os.path.isdir(full_path):
            recurse_files(full_path, files)

        if os.path.splitext(full_path)[1] == ".hlsl":
            files.append(full_path)


if __name__ == "__main__":
    print("Shader compiler\n(c) AG Games 2024")

    print("Checking for DXC... ", end="")
    if which("dxc") is None:
        print("Not found.")
        exit(1)

    print("Found.")

    pwd = os.getcwd()
    print("Current working directory: " + pwd)

    print("Scanning through files... ", end="")
    files = []

    recurse_files(pwd, files)

    print("Done.")
    print(f"Starting compilation of {len(files)} files.")

    VERTEX_ENTRYPOINT = "Vertex"
    PIXEL_ENTRYPOINT = "Pixel"

    for file in files:
        print(f"Compiling {file}... ", end="")
        file_name = os.path.splitext(file)[0]

        with open(file, 'r') as f:
            f_text = f.read()

            if VERTEX_ENTRYPOINT in f_text:
                print("Vertex... ", end="")
                output = Popen(["dxc", "-spirv", "-T", "vs_6_0", "-E", VERTEX_ENTRYPOINT, "-Fo", f"{file_name}_vert.spv", file], stdout=subprocess.PIPE)
                output.poll()
                print(output.returncode)
                if output.returncode != 0:
                    print("Failed.")
                    print(output.stdout.read())
                    exit(1)

            if PIXEL_ENTRYPOINT in f_text:
                print("Pixel... ", end="")
                output = Popen(["dxc", "-spirv", "-T", "ps_6_0", "-E", PIXEL_ENTRYPOINT, "-Fo", f"{file_name}_frag.spv", file], stdout=subprocess.PIPE)
                if output.returncode != 0:
                    print("Failed.")
                    print(output.stdout.read())

        print("Done.")
