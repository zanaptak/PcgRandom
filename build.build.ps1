# Install PowerShell for your platform:
#   https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell
# Install Invoke-Build:
#   https://github.com/nightroman/Invoke-Build
# At a PS prompt, run any build task, e.g.:
#   Invoke-Build Build
#   Invoke-Build Test
#   Invoke-Build ?  # this lists available tasks

task Clean {
  exec { dotnet clean .\src -c Release }
}

task Build {
  exec { dotnet build .\src -c Release }
}

task Pack Clean, Build, {
  exec { dotnet pack .\src -c Release }
}

task TestJs {
  Set-Location test\PcgRandom.Tests
  remove build
  exec { npm test }
}

task TestNet Clean, Build, {
  Set-Location test\PcgRandom.Tests
  exec { dotnet run -c Release }
}

task Test TestJs, TestNet

task Benchmark Clean, Build, {
  Set-Location .\benchmark
  exec { dotnet run -p PcgRandom.Benchmark.fsproj -c Release }
}

task . Build
