import os
#=====New Code====#
from openpyxl import Workbook
#=================#

dir_containing_files = "/var/shared/"

#=====New Code====#
dest_wb = Workbook()
#=================#

for root, dir, filenames in os.walk(dir_containing_files):
    for file in filenames:
        file_name = file.split('.')[0]
        # Absolute Path for Excel files
        file_path = os.path.abspath(os.path.join(root, file))

        #=====New Code====#

        # Create new sheet in destination Workbook
        dest_wb.create_sheet(file_name)
        dest_ws = dest_wb[file_name]
        #=================#

#=====New Code====#
dest_wb.save("yearly_sales.xlsx")
#=================#
