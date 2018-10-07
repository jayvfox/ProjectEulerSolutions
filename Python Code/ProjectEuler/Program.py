import time
import project_euler_problems

t1 = time.time()
print(project_euler_problems.problem141.solution()) 
t2 = time.time()
print("The solution took {} to compute".format(t2-t1))
