(async function() {
    const output = document.getElementById('output');
    try {
      const res = await fetch('/api/device/hostname');
      if (!res.ok) throw new Error('HTTP ' + res.status);
      const { hostname, ip } = await res.json();
      output.textContent = `Hostname = ${hostname} \nIP = ${ip}`;
    } catch (err) {
      output.textContent = 'Error: ' + err;
    }
  })();
  