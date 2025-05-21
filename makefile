init:
	dotnet build lib/Kirisoup.Diagnostics.TypeUsageRules/src/TypeUsageRules -c release \
		-p:PackDir=$(shell for %%A in (".pkg") do @echo %%~fA)
pack:
	if defined DIR ( \
	    dotnet build -c release -p:PackDir=$(shell for %%A in ("$(DIR)") do @echo %%~fA) \
	) else ( \
	    dotnet build -c release \
	)