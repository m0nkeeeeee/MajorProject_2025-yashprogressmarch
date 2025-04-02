import tkinter as tk
from tkinter import *
from qiskit import QuantumCircuit
import numpy as np
from qiskit.visualization import visualize_transition
import qiskit.visualization.exceptions as qv_exception

app = tk.Tk()
def center_window():
    app.update_idletasks()
    width = app.winfo_width()
    height = app.winfo_height()
    screen_width = app.winfo_screenwidth()
    screen_height = app.winfo_screenheight()
    x = (screen_width // 2) - (width // 2)
    y = (screen_height // 2) - (height // 2)
    app.geometry(f"{width}x{height}+{x}+{y}")

app.after(100, center_window)  # Call after the window is initialized

app.title('Quantum Visualizer')

button_font = ('Arial', 16, 'bold')
display_font = ("Arial", 20, 'bold')
heading_font = ("Arial", 28, 'bold')  # New font for the heading

background_color = '#2c3e50'
button_color = '#2980b9'
button_text_color = 'white'
display_color = '#34495e'
entry_color = '#2c3e50'

# Set background color for the entire app
app.configure(bg=background_color)

def clear():
    global circuit
    display.delete(0, END)
    initialize_circuit()

def initialize_circuit():
    global circuit 
    circuit = QuantumCircuit(1)

initialize_circuit()
theta = 0
gates_applied = []

def change_theta(num, window, circuit, key):
    global theta
    theta = num * np.pi
    if key == 'x':
        circuit.rx(theta, 0)
        theta = 0
    elif key == 'y':
        circuit.ry(theta, 0)
        theta = 0
    else:
        circuit.rz(theta, 0)
        theta = 0
    gates_applied.append(key)
    if len(gates_applied) >= 10:
        visualize_circuit(circuit, app)
        gates_applied.clear()
    window.destroy()

def display_gate(gate_input):
    display.insert(END, gate_input)

def user_input(circuit, key):
    get_input = tk.Tk()
    get_input.title('Get Theta')
    get_input.geometry('360x160')
    get_input.resizable(0, 0)
    get_input.configure(bg=background_color)

    values = [0.25, 0.5, 1.0, 2.0]
    for i, value in enumerate(values):
        button = tk.Button(get_input, text=f"PI/{value}", 
                          font=('Arial', 12),
                          bg=button_color, 
                          fg=button_text_color,
                          command=lambda val=value: change_theta(val, get_input, circuit, key))
        button.grid(row=0, column=i, padx=10, pady=15, sticky='we')

    entry = tk.Entry(get_input, width=20, font=('Arial', 12), bg='white')
    entry.grid(row=1, columnspan=len(values), padx=10, pady=15)

    # Configure column weights for proper spacing
    for i in range(len(values)):
        get_input.grid_columnconfigure(i, weight=1)

    get_input.mainloop()

def about():
    info = tk.Tk()
    info.title('About')
    info.geometry("650x470")
    info.resizable(0, 0)
    info.configure(bg=background_color)

    text = tk.Text(info, height=20, width=20, font=('Arial', 11), bg='#ecf0f1')

    label = tk.Label(info, text="About Quantum Visualizer", bg=background_color, fg='white')
    label.config(font=('Arial', 16, 'bold'))

    text_to_display = """
    About: Visualization tool for Single Qubit Rotation on Bloch Sphere
    
    Created by : Yash Chakraborty
    Created using: Python, Tkinter, Qiskit
    
    Info about the gate buttons and corresponding qiskit commands:
    
    X = flips the state of qubit -                                 circuit.x()
    Y = rotates the state vector about Y-axis -                    circuit.y()
    Z = flips the phase by PI radians -                            circuit.z()
    Rx = parameterized rotation about the X axis -                 circuit.rx()
    Ry = parameterized rotation about the Y axis.                  circuit.ry()
    Rz = parameterized rotation about the Z axis.                  circuit.rz()
    S = rotates the state vector about Z axis by PI/2 radians -    circuit.s()
    T = rotates the state vector about Z axis by PI/4 radians -    circuit.t()
    Sd = rotates the state vector about Z axis by -PI/2 radians -  circuit.sdg()
    Td = rotates the state vector about Z axis by -PI/4 radians -  circuit.tdg()
    H = creates the state of superposition -                       circuit.h()
    
    For Rx, Ry and Rz, 
    theta(rotation_angle) allowed range in the app is [-2*PI,2*PI]
    
    In case of a Visualization Error, the app closes automatically.
    This indicates that visualization of your circuit is not possible.
    
    At a time, only ten operations can be visualized.
    """

    label.pack(pady=10)
    text.pack(fill='both', expand=True, padx=20, pady=10)

    text.insert(END, text_to_display)
    info.mainloop()

def visualize_circuit(circuit, window):
    try:
        visualize_transition(circuit=circuit)
    except qv_exception.VisualizationError:
        pass

# Create a main container frame
main_container = tk.Frame(app, bg=background_color)
main_container.pack(fill='both', expand=True, padx=20, pady=20)

# Exit button at the top right
exit_button = tk.Button(main_container, text="Exit", font=button_font, bg='red', fg='white', command=app.destroy)
exit_button.pack(anchor='ne', pady=10, padx=10)

# Add the big heading at the top
heading_label = tk.Label(main_container, text="QUANTUM LOGIC GATE VISUALISER", 
                        font=heading_font, bg=background_color, fg='white')
heading_label.pack(pady=20)

# Display frame
display_frame = tk.LabelFrame(main_container, bg=background_color, borderwidth=0)
display_frame.pack(fill='x', pady=20)

# Display field
display = tk.Entry(display_frame, width=30, font=display_font, bg=display_color, fg='white', borderwidth=5, justify='left')
display.pack(pady=10, fill='x', padx=50)

# Button frame
button_frame = tk.Frame(main_container, bg=background_color)
button_frame.pack(pady=20)

gate_buttons = [
    ('X', lambda: [display_gate('x'), circuit.x(0)]),
    ('Y', lambda: [display_gate('y'), circuit.y(0)]),
    ('Z', lambda: [display_gate('z'), circuit.z(0)]),
    ('RX', lambda: [display_gate('Rx'), user_input(circuit, 'x')]),
    ('RY', lambda: [display_gate('Ry'), user_input(circuit, 'y')]),
    ('RZ', lambda: [display_gate('Rz'), user_input(circuit, 'z')]),
    ('S', lambda: [display_gate('s'), circuit.s(0)]),
    ('SD', lambda: [display_gate('SD'), circuit.sdg(0)]),
    ('H', lambda: [display_gate('H'), circuit.h(0)]),
    ('T', lambda: [display_gate('t'), circuit.t(0)]),
    ('TD', lambda: [display_gate('TD'), circuit.tdg(0)])
]

# Create a grid of gate buttons with better spacing
for i, (text, command) in enumerate(gate_buttons):
    row, col = i // 3, i % 3
    button = tk.Button(button_frame, text=text, font=button_font, bg=button_color, fg=button_text_color, 
                     command=command, width=10, height=3)
    button.grid(row=row, column=col, padx=15, pady=15)

# Place the About button in a better position
about_button = tk.Button(button_frame, text='About', font=button_font, bg='#8e44ad', fg='white', command=about, width=10, height=3)
about_button.grid(row=3, column=2, padx=15, pady=15)

# Action buttons with consistent sizing
action_button_frame = tk.Frame(main_container, bg=background_color)
action_button_frame.pack(pady=10)

visualize_button = tk.Button(action_button_frame, text='Visualize', font=button_font, bg='#27ae60', fg='white', 
                           command=lambda: visualize_circuit(circuit, app), width=40, height=30)
visualize_button.pack(pady=2)

clear_button = tk.Button(action_button_frame, text='Clear', font=button_font, bg='#c0392b', fg='white', 
                       command=clear, width=15, height=2)
clear_button.pack(pady=5)

quit_button = tk.Button(action_button_frame, text='Quit', font=button_font, bg='#7f8c8d', fg='white', 
                      command=app.destroy, width=15, height=2)
quit_button.pack(pady=5)

# Set the window to fullscreen
app.attributes('-fullscreen', True)

app.mainloop()