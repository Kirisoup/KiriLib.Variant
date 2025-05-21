dep:
	dotnet build lib/Kirisoup.Diagnostics.TypeUsageRules/src/TypeUsageRules -c release \
		-p:PackDir=$(shell for %%A in (".pkg") do @echo %%~fA)
