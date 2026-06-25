console.log("🏥 MediCore Hospital System Loaded");

const API = "http://localhost:5000/api";

/* =========================
   🔥 PATIENT MODULE
========================= */

async function loadPatients() {
    const res = await fetch(`${API}/Patient/all`);
    const data = await res.json();

    let rows = "";

    data.forEach(p => {
        rows += `
        <tr>
            <td>${p.patientId}</td>
            <td>${p.name}</td>
            <td>${p.age}</td>
            <td>${p.gender}</td>
            <td>${p.phone}</td>
            <td>
                <button onclick="editPatient(${p.patientId}, '${p.name}', ${p.age}, '${p.gender}', '${p.phone}', '${p.address}', '${p.status}')">Edit</button>
                <button onclick="deletePatient(${p.patientId})">Delete</button>
            </td>
        </tr>`;
    });

    const tbody = document.querySelector("#patientTable tbody");
    if (tbody) {
        tbody.innerHTML = rows;
    }
}

async function savePatient(e) {
    e.preventDefault();

    const patient = {
        name: document.getElementById("name").value,
        age: Number(document.getElementById("age").value),
        gender: document.getElementById("gender").value,
        phone: document.getElementById("phone").value,
        address: document.getElementById("address").value,
        status: document.getElementById("status").value || "Active"
    };

    console.log("SENDING DATA:", patient); // 🔥 IMPORTANT DEBUG

    const res = await fetch(`${API}/Patient/add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(patient)
    });

    const text = await res.text();

    if (!res.ok) {
        console.log("❌ BACKEND ERROR:", text);
        alert("Add Failed - Check Console");
    } else {
        alert("Patient Added Successfully");
        document.getElementById("patientForm").reset();
        loadPatients();
    }
}

async function deletePatient(id) {
    await fetch(`${API}/Patient/delete/${id}`, { method: "DELETE" });
    loadPatients();
}

function editPatient(id, name, age, gender, phone, address, status) {
    document.getElementById("patientId").value = id;
    document.getElementById("name").value = name;
    document.getElementById("age").value = age;
    document.getElementById("gender").value = gender;
    document.getElementById("phone").value = phone;
    document.getElementById("address").value = address;
    document.getElementById("status").value = status;
}

/* =========================
   👨‍⚕️ DOCTOR MODULE
========================= */

async function saveDoctor(e) {
    e.preventDefault();

    const doctor = {
        doctorName: document.getElementById("doctorName").value,
        specialization: document.getElementById("specialization").value,
        phone: document.getElementById("doctorPhone").value
    };

    const res = await fetch(`${API}/Doctor/add`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(doctor)
    });

    if (res.ok) {
        alert("Doctor Added");
        document.getElementById("doctorForm").reset();
        loadDoctors();
    }
}

async function loadDoctors() {
    const res = await fetch(`${API}/Doctor/all`);
    const data = await res.json();

    let rows = "";

    data.forEach(d => {
        rows += `
        <tr>
            <td>${d.doctorId}</td>
            <td>${d.doctorName}</td>
            <td>${d.specialization}</td>
            <td>${d.phone}</td>
        </tr>`;
    });

    const tbody = document.querySelector("#doctorTable tbody");
    if (tbody) {
        tbody.innerHTML = rows;
    }
}

/* =========================
   📅 APPOINTMENT MODULE
========================= */

async function saveAppointment(e) {
    e.preventDefault();

    const appointment = {
    patientId: parseInt(document.getElementById("patientId").value),
    doctorId: parseInt(document.getElementById("doctorId").value),
    appointmentDate: document.getElementById("appointmentDate").value,
    appointmentTime: document.getElementById("appointmentTime").value,
    status: "Scheduled"
};

    const res = await fetch(`${API}/Appointment/add`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(appointment)
    });

    if (res.ok) {
        alert("Appointment Booked");
        loadAppointments();
    }
}

async function loadAppointments() {
    const res = await fetch(`${API}/Appointment/all`);
    const data = await res.json();

    let rows = "";

    data.forEach(a => {
        const formattedDate = a.appointmentDate ? a.appointmentDate.substring(0, 10) : "";
        rows += `
        <tr>
            <td>${a.appointmentId}</td>
            <td>${a.patientId}</td>
            <td>${a.doctorId}</td>
            <td>${formattedDate}</td>
            <td>${a.appointmentTime}</td>
            <td>${a.status}</td>
            <td>
                <button onclick="cancelAppointment(${a.appointmentId})">Cancel</button>
            </td>
        </tr>`;
    });

    const tbody = document.querySelector("#apptTable tbody");
    if (tbody) {
        tbody.innerHTML = rows;
    }
}

async function cancelAppointment(id) {
    await fetch(`${API}/Appointment/cancel/${id}`, {
        method: "PUT"
    });

    loadAppointments();
}

/* =========================
   📄 MEDICAL RECORDS
========================= */

async function saveRecord(e) {
    e.preventDefault();

    const record = {
        patientId: parseInt(document.getElementById("recordPatientId").value),
        diagnosis: document.getElementById("diagnosis").value,
        description: document.getElementById("description").value,
        prescription: document.getElementById("prescription").value,
        visitDate: document.getElementById("visitDate").value
    };

    const res = await fetch(`${API}/MedicalRecord/add`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(record)
    });

    if (res.ok) {
        alert("Record Added");
        loadRecords();
    }
}

async function loadRecords() {
    const res = await fetch(`${API}/MedicalRecord/all`);
    const data = await res.json();

    let rows = "";

    data.forEach(r => {
        const formattedDate = r.visitDate ? r.visitDate.substring(0, 10) : "";
        rows += `
        <tr>
            <td>${r.recordId}</td>
            <td>${r.patientId}</td>
            <td>${r.diagnosis}</td>
            <td>${r.description || ""}</td>
            <td>${r.prescription || ""}</td>
            <td>${formattedDate}</td>
        </tr>`;
    });

    const tbody = document.querySelector("#recordTable tbody");
    if (tbody) {
        tbody.innerHTML = rows;
    }
}

/* =========================
   🚀 INIT LOAD
========================= */

loadPatients();
loadDoctors();
loadAppointments();
loadRecords();