#!/usr/bin/python3
# AG Games Auto-HLSL-To-Spirv Shader Compiler 1.0
# Works in the current working directory, so make sure the current directory is the one with the shaders you wish to
# compile.
# While it will recurse all available subdirectories as well, this can take a lot of time if there are a lot of files.
# This may be updated later to support choosing a directory.

import os
from subprocess import run
from shutil import which


def recurse_files(path: str, files: list[str]):
    for file in os.listdir(path):
        full_path = os.path.join(path, file)

        if os.path.isdir(full_path):
            recurse_files(full_path, files)

        if os.path.splitext(full_path)[1] == ".hlsl":
            files.append(full_path)


if __name__ == "__main__":
    print("AG Games Shader Compiler\n(c) AG Games 2024")

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
                output = run(["dxc", "-spirv", "-T", "vs_6_0", "-E", VERTEX_ENTRYPOINT, "-Fo", f"{file_name}_vert.spv", file], capture_output=True)
                if output.returncode != 0:
                    print("Failed.")
                    print(output.stderr.decode('UTF-8'))
                    exit(1)

            if PIXEL_ENTRYPOINT in f_text:
                print("Pixel... ", end="")
                output = run(["dxc", "-spirv", "-T", "ps_6_0", "-E", PIXEL_ENTRYPOINT, "-Fo", f"{file_name}_frag.spv", file], capture_output=True)
                if output.returncode != 0:
                    print("Failed.")
                    print(output.stderr.decode('UTF-8'))
                    exit(1)

        print("Done.")

    print("All done. Compilation successful for all shaders.")
