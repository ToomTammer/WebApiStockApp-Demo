1.สร้าง sonarqube docker-compose สำหรับเข้าหน้า monitor sonarqube

เข้าครั้งแรก user: amin, pass: admin

### network
ช่วยให้การจัดการการสื่อสารระหว่างคอนเทนเนอร์เป็นไปอย่างมีประสิทธิภาพและปลอดภัย.
<br>
ประเภทเครือข่ายใน Docker:<br>
<br>

### 1.Bridge Network (ค่าเริ่มต้น):<br>
ใช้สำหรับการเชื่อมต่อคอนเทนเนอร์ภายในเครื่องเดียวกัน
คอนเทนเนอร์สามารถสื่อสารกันโดยใช้ชื่อคอนเทนเนอร์หรือ IP Address
<br>

### 2.Host Network:<br>
คอนเทนเนอร์ใช้ IP และพอร์ตของโฮสต์
ไม่แยกเครือข่ายออกจากโฮสต์
<br>

### 3.Overlay Network:<br>
ใช้ใน Docker Swarm หรือ Docker Compose ที่มีหลายโฮสต์
ช่วยให้คอนเทนเนอร์บนโฮสต์ต่างๆ สามารถสื่อสารกันได้
<br>

### 4.Macvlan Network:<br>
ให้คอนเทนเนอร์มี MAC Address ของตัวเอง
ใช้ในกรณีที่ต้องการให้คอนเทนเนอร์ดูเหมือนอุปกรณ์จริงในเครือข่าย
<br>

### 5.None Network:<br>
ปิดการเชื่อมต่อเครือข่ายทั้งหมดสำหรับคอนเทนเนอร์
<br>

### sonar scanner สำรับ ส่ง code ไป scanner<br>
docker-compose-scan.yml
<br>

### sonar-project.properties สำรับ ส่ง code ไป scanner<br>
Certainly! Here's a brief summary:
<br>

### 1.Create sonar-project.properties: <br>
Place it in your project root with SonarQube configuration.

### 1.Docker Configuration:<br>

Your Docker setup mounts the current directory to /usr/src in the container.
Ensure sonar-project.properties is in the project root, so it will be accessible at /usr/src/sonar-project.properties.
<br>

### 1.Run SonarScanner:<br>
The sonar-scanner command will automatically use sonar-project.properties from /usr/src if it’s present.