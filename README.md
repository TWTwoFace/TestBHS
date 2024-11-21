# TestBHS

# 1 Задание
Добавлена кнопка 'Load' для демонстрации загрузки данных по нажатию

Если вызывать загрузку в методе 'Start' класса 'Bootstrap', то до первого кадра успеет загрузиться большая часть данных.

Для демонстрации возможности начинать загрузку данных в методе старт добавлено поле 'Load On Start' в классе Bootstrap, с возможностью задать его в инспекторе (помечено атрибутом SerializeField)

# 2 Задание
Выполнен рефакторинг класса Projectile в соответствии с заданием. Реализации React() в классах, реализующих интерфейс IHaveProjectileReaction, сделана в соответсвии с той логикой, что была в OnCollisionEnter (Projectile) до рефакторинга (посчитал менять логику объектов излишним. Такой код легче просто переписать). На префабы из которых состоит сцена (стены, пол, потолок и т.д.) добавлен компонент DecalPlacer, реализующий интерфейс IHaveProjectileReaction, чтобы объекты сами занимались логикой отображения декалей и VFX'ов.

'm' - modified

'\+' - new file (with path)

Files:

m Projectile.cs

m GasTankScript.cs

m ExplosiveBarrelScript.cs

m TargetScript.cs

\+ DecalPlacer.cs (Assets/Infima Games/Low Poly Shooter Pack - Free Sample/Code/Legacy/)
  
\+ IHaveProjectileReaction.cs (Assets/Infima Games/Low Poly Shooter Pack - Free Sample/Code/Legacy/)
  
