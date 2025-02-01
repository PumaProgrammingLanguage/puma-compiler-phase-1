
import os

def create_index_file(directory):
    index_file_path = os.path.join(directory, 'index.html')
    if os.path.exists(index_file_path):
        os.remove(index_file_path)

    with open(index_file_path, 'w') as f:
        f.write(f"<html><body><h1>Index of {directory}</h1><ul>")
        for item in os.listdir(directory):
            item_path = os.path.join(directory, item)
            if os.path.isdir(item_path):
                if item.startswith('.'):
                    continue
                f.write(f"<li>Folder: {item}<ul>")
                list_sub_items(item_path, f)
                f.write("</ul></li>")
            else:
                f.write(f"<li>File: {item}</li>")
        f.write("</ul></body></html>")
    print(f"Created index.html in {directory}")

def list_sub_items(directory, file_handle):
    for item in os.listdir(directory):
        item_path = os.path.join(directory, item)
        if os.path.isdir(item_path):
            if item.startswith('.'):
                continue
            file_handle.write(f"<li>Subfolder: {item}<ul>")
            list_sub_items(item_path, file_handle)
            file_handle.write("</ul></li>")
        else:
            file_handle.write(f"<li>File: {item}</li>")

def main():
    root_directory = os.getcwd()
"""
    for dirpath, dirnames, filenames in os.walk(root_directory):
        # Skip directories that start with a dot
        dirnames[:] = [d for d in dirnames if not d.startswith('.')]
        """
        create_index_file(root_directory)

if __name__ == "__main__":
    main()
